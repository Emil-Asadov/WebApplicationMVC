using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_DB.View;

namespace WebApplication_DB.Models
{
    public class ClassGetMethods : IGetMethods
    {
        private readonly ClassDbConfig clsDbConfig = new ClassDbConfig();

        public (DataTable dt, string err) GetCustomer(int custID = 0)
        {
            var dtRes = new DataTable();
            var errRes = string.Empty;
            var query = "SBNK_PRL.PKG_CUSTOMERS_E01.GET_CUSTOMERS_E01";
            try
            {
                var V_RES = new OracleParameter
                {
                    ParameterName = "V_RES",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.ReturnValue
                };
                var P_IDN = new OracleParameter
                {
                    ParameterName = "P_IDN",
                    OracleDbType = OracleDbType.Int32,
                    Value = custID
                };
                OracleParameter[] arr = new OracleParameter[] { V_RES };
                if (custID > 0)
                    arr = new OracleParameter[] { V_RES, P_IDN };
                (dtRes, errRes) = clsDbConfig.FillDT(query, arr);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (dtRes, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }

            return (dtRes, errRes);
        }

        public (DataTable dt, string err) GetCustomer(ClassControls cls)
        {
            var dtRes = new DataTable();
            var errRes = string.Empty;
            var query = "SELECT * FROM SBNK_PRL.VIEW_CUSTOMERS_E01 T WHERE";
            try
            {
                if (cls.custIdn != 0)
                    query += $" T.IDN={cls.custIdn}";
                if (!string.IsNullOrWhiteSpace(cls.name))
                    query += query.Contains("T.") ? $" AND LOWER(T.NAME) like LOWER('%{cls.name}%')" : $" LOWER(T.NAME) like LOWER('%{cls.name}%')";
                if (!string.IsNullOrWhiteSpace(cls.surname))
                    query += query.Contains("T.") ? $" AND LOWER(T.SURNAME) like LOWER('%{cls.surname}%')" : $" LOWER(T.SURNAME) like LOWER('%{cls.surname}%')";
                if (!string.IsNullOrWhiteSpace(cls.birthPlace))
                    query += query.Contains("T.") ? $" AND LOWER(T.BIRTH_PLACE) like LOWER('%{cls.birthPlace}%')" : $" LOWER(T.BIRTH_PLACE) like LOWER('%{cls.birthPlace}%')";
                if (cls.birthDate != "  .  .")
                    query += query.Contains("T.") ? $" AND T.BIRTH_DATE=TO_DATE('{cls.birthDate:dd.MM.yyyy}','DD.MM.YYYY')" : $" T.BIRTH_DATE=TO_DATE('{cls.birthDate:dd.MM.yyyy}','DD.MM.YYYY')";
                if (cls.gender != -1)
                    query += query.Contains("T.") ? $" AND T.GENDER={cls.gender}" : $" T.GENDER={cls.gender}";
                if (!string.IsNullOrWhiteSpace(cls.docNo))
                    query += query.Contains("T.") ? $" AND LOWER(T.DOC_NO) like LOWER('%{cls.docNo}%')" : $" LOWER(T.DOC_NO) like LOWER('%{cls.docNo}%')";
                if (!string.IsNullOrWhiteSpace(cls.finCode))
                    query += query.Contains("T.") ? $" AND LOWER(T.FIN_CODE) like LOWER('%{cls.finCode}%')" : $" LOWER(T.FIN_CODE) like LOWER('%{cls.finCode}%')";
                if (!string.IsNullOrWhiteSpace(cls.phoneNumber))
                    query += query.Contains("T.") ? $" AND LOWER(T.PHONE_NUMBER) like LOWER('%{cls.phoneNumber}%')" : $" LOWER(T.PHONE_NUMBER) like LOWER('%{cls.phoneNumber}%')";
                if (!string.IsNullOrWhiteSpace(cls.email))
                    query += query.Contains("T.") ? $" AND LOWER(T.EMAIL) like LOWER('%{cls.email}%')" : $" LOWER(T.EMAIL) like LOWER('%{cls.email}%')";

                (dtRes, errRes) = clsDbConfig.FillDT(query);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (dtRes, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }

            return (dtRes, errRes);
        }

        public (ClassControls res, string err) GetCustomerById(int custId)
        {
            var cls = new ClassControls();
            var errRes = string.Empty;
            var dt = new DataTable();
            try
            {
                (dt, errRes) = GetCustomer(custId);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (null, errRes);
                cls = new ClassControls
                {
                    custIdn = int.Parse(dt.Rows[0]["IDN"].ToString()),
                    name = dt.Rows[0]["NAME"].ToString(),
                    surname = dt.Rows[0]["SURNAME"].ToString(),
                    birthPlace = dt.Rows[0]["BIRTH_PLACE"].ToString(),
                    birthDate = dt.Rows[0]["BIRTH_DATE"].ToString(),
                    genderName = dt.Rows[0]["GENDER_NAME"].ToString(),
                    docNo = dt.Rows[0]["DOC_NO"].ToString(),
                    finCode = dt.Rows[0]["FIN_CODE"].ToString(),
                    phoneNumber = dt.Rows[0]["PHONE_NUMBER"].ToString(),
                    email = dt.Rows[0]["EMAIL"].ToString(),
                    gender=int.Parse(dt.Rows[0]["GENDER"].ToString())
                };
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
                return (null, errRes);
            }
            return (cls, errRes);
        }

        public (string fullName, string err) GetCustomerFullNameById(int custId)
        {
            var fullNameOut = string.Empty;
            var errRes = string.Empty;
            var query = $"SELECT SBNK_PRL.PKG_CUSTOMERS_E01.GET_CUSTOMER_FULL_NAME(P_CUST_ID => {custId})  FROM DUAL";
            try
            {
                (fullNameOut, errRes) = clsDbConfig.FillValue(query);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (fullNameOut, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }

            return (fullNameOut, errRes);
        }

        public (List<ClassControls> res, string err) GetCustomerLast()
        {
            var lst = new List<ClassControls>();
            var errRes = string.Empty;
            var dt = new DataTable();
            try
            {
                (dt, errRes) = GetCustomer();
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (null, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
                return (null, errRes);
            }
            var cls = new ClassControls();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lst.Add(
                        new ClassControls
                        {
                            custIdn = int.Parse(dt.Rows[i]["IDN"].ToString()),
                            name = dt.Rows[i]["NAME"].ToString(),
                            surname = dt.Rows[i]["SURNAME"].ToString(),
                            birthPlace = dt.Rows[i]["BIRTH_PLACE"].ToString(),
                            birthDate = dt.Rows[i]["BIRTH_DATE"].ToString(),
                            genderName = dt.Rows[i]["GENDER_NAME"].ToString(),
                            docNo = dt.Rows[i]["DOC_NO"].ToString(),
                            finCode = dt.Rows[i]["FIN_CODE"].ToString(),
                            phoneNumber = dt.Rows[i]["PHONE_NUMBER"].ToString(),
                            email = dt.Rows[i]["EMAIL"].ToString()
                        }
                    );
            };

            return (lst, errRes);
        }

        public (DataTable dt, string err) GetGender()
        {
            var dtOut = new DataTable();
            var errOut = string.Empty;
            var query = "SBNK_PRL.PKG_CUSTOMERS_E01.GET_GENDER";
            try
            {
                var V_RES = new OracleParameter
                {
                    ParameterName = "V_RES",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.ReturnValue
                };

                (dtOut, errOut) = clsDbConfig.FillDT(query, new OracleParameter[] { V_RES });
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (dtOut, errOut);
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }

            return (dtOut, errOut);
        }
    }
}
