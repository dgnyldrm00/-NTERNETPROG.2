using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using sorucevapportalı.Models;


namespace sorucevapportalı.Controllers
{
    public class soruDBController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @" 
                select soruID,soru from dbo.soruDB


                ";
            DataTable table = new DataTable();
            using (var con= new SqlConnection(ConfigurationManager.ConnectionStrings["sorucevapportalDB"].ConnectionString))
                using (var cmd= new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);

            }
              return Request.CreateResponse(HttpStatusCode.OK,table);
            
            
        }
        public string Post(soruDB sor)
        {
            try
            {
                string query = @"
                   insert into dbo.soruDB values
                    ('"+sor.soru+@"')
                   ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["sorucevapportalDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "başarılı şekilde eklendi!!";
            }
            catch (Exception)
            {

                return "başarılı bir şekilde eklenemedi!!";
            }
        }

        public string Put(soruDB sor)
        {
            try
            {
                string query = @"
                   update dbo.soruDB set soru=
                    '" + sor.soru + @"'
                        where soruID="+sor.soruID+@"
                   ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["sorucevapportalDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "başarılı şekilde güncellendi!!";
            }
            catch (Exception)
            {

                return "başarılı bir şekilde güncellenemedi!!";
            }
        }
        public string Delete(int id)
        {
            try
            {
                string query = @"
                   delete from dbo.soruDB 
                        where soruID=" + id + @"
                   ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["sorucevapportalDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "başarılı şekilde silindi!!";
            }
            catch (Exception)
            {

                return "başarılı bir şekilde silinemedi!!";
            }
        }
    }
}
