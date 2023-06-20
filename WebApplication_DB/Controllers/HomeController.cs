using CustomerProject.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_DB.Models;
using WebApplication_DB.View;

namespace WebApplication_DB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetMethods clsGetMethods = new ClassGetMethods();
        private readonly IPostMethods clsPostMethods = new ClassPostMethods();

        public ActionResult Index()
        {
            var res = new List<ClassControls>();
            var err = string.Empty;
            try
            {
                (res, err) = clsGetMethods.GetCustomerLast();
                if (!string.IsNullOrWhiteSpace(err))
                {
                    return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = err });
                }

                return View(res);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            ViewBag.Message = err;
            return View();
        }
        
        [HttpGet]
        public ActionResult Create(string inpParam)
        {
            var cls = new ClassControls();
            try
            {
                cls.lstGender = new List<ClassGender>();
                (var dt, var err) = clsGetMethods.GetGender();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cls.lstGender.Add(
                    new ClassGender
                    {
                        idn = int.Parse(dt.Rows[i]["ID"].ToString()),
                        name = dt.Rows[i]["Name"].ToString()
                    }
                );
                }
                #region View-da nezere almisam
                //res.lstGender.Insert(0, new ClassGender
                //{
                //    idn = 0,
                //    name = "Please select one"
                //});
                #endregion
                return View(cls);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = ex.Message });
            }
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateEmployee(ClassControls cls)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(cls);
                cls.lstGender = new List<ClassGender>();
                (var dt, var err) = clsGetMethods.GetGender();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cls.lstGender.Add(
                    new ClassGender
                    {
                        idn = int.Parse(dt.Rows[i]["ID"].ToString()),
                        name = dt.Rows[i]["Name"].ToString()
                    }
                );
                }
                #region View-da nezere almisam
                //res.lstGender.Insert(0, new ClassGender
                //{
                //    idn = 0,
                //    name = "Please select one"
                //});
                #endregion
                #region ClassControls-da Required qoyulub
                //if (cls.gender == 0)
                //{
                //    //Gender combobox-da 0-ci Index secili olduquna gore Required isdemir
                //    ModelState.AddModelError("gender", "Cinsi daxil edin");
                //    return View(cls);
                //}
                #endregion
                (var res, var custId, var errRes) = clsPostMethods.FileOper(cls);
                if (!string.IsNullOrWhiteSpace(errRes))
                {
                    return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = errRes });
                }
                if (res != "4")
                {
                    TempData["!Res_4"] = res;
                    return View(cls);
                }
                ModelState.Clear();
                TempData["Res_4"] = $"Əməliyyat yerinə yetirildi. {custId} qeydiyyatlı müştəri əlavə edildi";

                return RedirectToAction("Create", "Home", new { inpParam = "Test" }); //Eger Get metodunun giris parametri varsa(string inpParam), parametr bu sekilde oturulur: new { inpParam = "Test" }. Giris parametrinin adini eynile burda yazmaq lazimdir. Eks halda null gedecey
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var res = new ClassControls();
            var err = string.Empty;
            try
            {
                if (id == 0)
                {
                    return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = err });
                }
                (res, err) = clsGetMethods.GetCustomerById(id);
                if (!string.IsNullOrWhiteSpace(err))
                {
                    return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = err });
                }
                if (res == null)
                {
                    return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = err });
                }
                res.lstGender = new List<ClassGender>();
                (var dtGender, var errGender) = clsGetMethods.GetGender();
                for (int i = 0; i < dtGender.Rows.Count; i++)
                {
                    res.lstGender.Add(
                    new ClassGender
                    {
                        idn = int.Parse(dtGender.Rows[i]["ID"].ToString()),
                        name = dtGender.Rows[i]["NAME"].ToString()
                    }
                );
                }

                #region View-da nezere almisam
                //res.lstGender.Insert(0, new ClassGender
                //{
                //    idn = 0,
                //    name = "Please select one"
                //});
                #endregion

                #region ComboBox-n doldurulmasini Create View-da oldugu kimi yazmisam
                //ViewBag.GenderId = new SelectList(cls.lstGender, "idn", "name");
                #endregion

                return View(res);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult UpdateCustomer(ClassControls cls)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(cls);
                cls.lstGender = new List<ClassGender>();
                (var dt, var err) = clsGetMethods.GetGender();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cls.lstGender.Add(
                    new ClassGender
                    {
                        idn = int.Parse(dt.Rows[i]["ID"].ToString()),
                        name = dt.Rows[i]["Name"].ToString()
                    }
                );
                }

                #region View-da nezere almisam
                //res.lstGender.Insert(0, new ClassGender
                //{
                //    idn = 0,
                //    name = "Please select one"
                //});
                #endregion

                #region ComboBox-n doldurulmasini Create View-da oldugu kimi yazmisam
                //ViewBag.GenderId = new SelectList(cls.lstGender, "idn", "name");
                #endregion

                #region ClassControls-da Required qoyulub
                //if (cls.gender == 0)
                //{
                //    //Gender combobox-da 0-ci Index secili olduquna gore Required isdemir
                //    ModelState.AddModelError("gender", "Cinsi daxil edin");
                //    return View(cls);
                //}
                #endregion

                (var res, var custId, var errRes) = clsPostMethods.FileOper(cls);
                if (!string.IsNullOrWhiteSpace(errRes))
                {
                    return RedirectToAction("Error", new ErrorViewModel { ErrorId = 400, ErrorMessage = errRes });
                }
                ModelState.Clear();
                TempData["Success"] = $"Əməliyyat yerinə yetirildi.{(char)13}{(char)10}{custId} qeydiyyatlı müştəri yeniləndi";

                return RedirectToAction("Edit", "Home", new { id = cls.custIdn }); //Eger Get metodunun giris parametri varsa(string inpParam), parametr bu sekilde oturulur: new { inpParam = "Test" }. Giris parametrinin adini eynile burda yazmaq lazimdir. Eks halda null gedecey
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", new ErrorViewModel { ErrorId = 400, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var res = new ClassControls();
            var err = string.Empty;
            try
            {
                if (id == 0)
                {
                    return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = err });
                }
                (res, err) = clsGetMethods.GetCustomerById(id);
                if (!string.IsNullOrWhiteSpace(err))
                {
                    return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = err });
                }
                if (res == null)
                {
                    return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = err });
                }
                return View(res);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = ex.Message });
            }
        }       
        

        [HttpGet, ActionName("Delete")]
        public ActionResult DeleteEmployeeGet(int id)
        {
            var res = new ClassControls();
            var err = string.Empty;
            try
            {
                if (id == 0)
                {
                    return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = err });
                }
                (res, err) = clsGetMethods.GetCustomerById(id);
                if (!string.IsNullOrWhiteSpace(err))
                {
                    return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = err });
                }
                if (res == null)
                {
                    return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = err });
                }
                return View(res);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = ex.Message });
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                (var res, var errRes) = clsPostMethods.FileDelete(id);
                if (!string.IsNullOrWhiteSpace(errRes))
                {
                    return RedirectToAction("Error","Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = errRes });
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error","Home", new ErrorViewModel { ErrorId = 400, ErrorMessage = ex.Message });
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Error(ErrorViewModel model)
        {
            //******************Emil************************
            if (model.ErrorId == 0)
                model.ErrorId = 500;
            if (model.ErrorMessage is null)
                model.ErrorMessage = "Internal Server Error";
            //model.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(model);
            //**********************************************           
        }
    }
}