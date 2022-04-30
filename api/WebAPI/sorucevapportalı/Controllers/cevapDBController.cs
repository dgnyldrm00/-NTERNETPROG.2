using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using sorucevapportalı.Models;
using System.Configuration;
using System.Web;

namespace sorucevapportalı.Controllers
{
    public class CevapDBController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select cevapId,cevaplayanisim,cevap,
                    soru,
                    cevaplayanfoto
                    from
                    dbo.CevapDB
                    ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["sorucevapportalDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);


        }

        public string Post(cevapDB emp)
        {
            try
            {
                string query = @"
                    insert into dbo.Employee values
                    (
                    '" + emp.cevaplayanisim + @"'
                    ,'" + emp.cevap + @"'
                    ,'" + emp.soru + @"'
                    ,'" + emp.cevaplayanfoto + @"'
                    )
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["sorucevapportalDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Başarıyla eklendi!!";
            }
            catch (Exception)
            {

                return "Eklenmedi!!";
            }
        }


        public string Put(cevapDB emp)
        {
            try
            {
                string query = @"
                    update dbo.cevapDB set 
                    cevaplayanisim='" + emp.cevaplayanisim + @"'
                    ,cevap='" + emp.cevap + @"'
                    ,soru='" + emp.soru + @"'
                    ,cevaplayanfoto='" + emp.cevaplayanfoto + @"'
                    where cevapID=" + emp.cevapID + @"
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["sorucevapportalDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Başarıyla güncellendi!!";
            }
            catch (Exception)
            {

                return "Güncellenmedi!!";
            }
        }


        public string Delete(int id)
        {
            try
            {
                string query = @"
                    delete from dbo.cevapDB
                    where cevapID=" + id + @"
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["sorucevapportalDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Başarıyla silindi!!";
            }
            catch (Exception)
            {

                return "silinmedi!!";
            }
        }

        [Route("api/cevapDB/GetAllCevapNames")]
        [HttpGet]
        public HttpResponseMessage GetAllCevapNames()
        {
            string query = @"
                    select cevaplayanisim from dbo.cevapDB";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["sorucevapportalDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/cevapDB/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);

                postedFile.SaveAs(physicalPath);

                return filename;
            }
            catch (Exception)
            {

                return "anonymous.png";
            }
        }
    }
}
