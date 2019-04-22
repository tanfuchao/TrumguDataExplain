using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrumguDataExplain.Core.Domain;
using TrumguDataExplain.Service;
using TrumguDataExplain.Web.Models.DataTables;

namespace TrumguDataExplain.Web.Controllers
{
    public class HomeController : Controller
    {
        #region view
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Index()
        {
            var session = Session["userInfo"] as UserInfo;
            if (session == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.RealName = session.RealName;
            ViewBag.Time = DateTime.Today.ToString("yyyy年MM月dd日");
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Remove("userInfo");
            return RedirectToAction("Login");
        }
        #endregion

        #region 登录请求
        [HttpPost]
        public JsonResult CheckLogin(string name, string pwd, bool remember)
        {
            IBaseBll<UserInfo> userBll = new BaseBll<UserInfo>();
            var userInfo = userBll.QueryByIf(m => m.LoginName == name && m.Password == pwd).FirstOrDefault();

            #region 逻辑判断

            if (userInfo == null || userInfo.Flag != 1)
            {
                //用户为空 或者 用户禁用 --不许登录
                return Json(false);
            }

            Session.Add("userInfo", userInfo);
            if (!remember)
            {
                //不需要记住密码 登录跳转
                CookieHelper.ClearCookie("name");
                CookieHelper.ClearCookie("pwd");
                return Json(true);
            }
            #endregion

            #region cookie存储
            //将用户名密码存入cookies
            CookieHelper.SetCookie("name",name,DateTime.Now.AddDays(7.0));
            CookieHelper.SetCookie("pwd", pwd, DateTime.Now.AddDays(7.0));
            
            #endregion

            //需要记住密码 登录跳转
            return Json(true);
        } 
        #endregion

        #region DataTables相关
        /// <summary>
        /// dataTable 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsonResult GetTableData(DataTablesParameters parameters)
        {
            IBaseBll<FieldInfo> fieldBll = new BaseBll<FieldInfo>();
            var list = fieldBll.QueryByIf(m => m.TableId == Convert.ToInt32(parameters.TableId)).ToList();
            var count = list.Count();
            return DataTablesJson(parameters.Draw, count, count, list);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="draw">请求次数计数器</param>
        /// <param name="recordsTotal">总共记录数</param>
        /// <param name="recordsFiltered">过滤后的记录数</param>
        /// <param name="data">数据</param>
        /// <param name="error">服务器错误信息</param>
        public JsonResult DataTablesJson<TEntity>(int draw, int recordsTotal, int recordsFiltered,
            IReadOnlyList<TEntity> data, string error = null)
        {
            var result = new DataTablesResult<TEntity>(draw, recordsFiltered, recordsFiltered, data);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region JsTree相关
        public JsonResult GetTreeData()
        {
            IBaseBll<TableInfo> tableBll = new BaseBll<TableInfo>();
            var treeNodes = tableBll.QueryAll().Select(m => new
            {
                id = m.Id,
                parent = m.ParentId.ToString() == "0" ? "#" : m.ParentId.ToString(),
                text = m.NameCh,
                icon = m.TreeIcon
            });
            return Json(treeNodes, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 表基础表格赋值
        public JsonResult GetTableInfo(int id)
        {
            IBaseBll<TableInfo> tableBll = new BaseBll<TableInfo>();
            var info = tableBll.QueryById(id);
            string tableStatus = info.TableState==1?"正常" : "禁用";
            return Json(new
            {
                table_name_cn =info.NameCh,
                table_name = info.NameEn,
                table_type = info.TableType,
                key_name = info.TableKey,
                table_status = tableStatus,
                unique_index = info.TableIndex,
                description = info.TableExplain
            });
        } 
        #endregion

    }
}