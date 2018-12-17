using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Sample05
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            try
            {
                // 加一条数据
                //test_insert();
                //  加多条数据
                //test_mult_insert();
                //  删除一条数据
                //test_del();
                //  删除多条数据
                test_mult_del();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }

        }

        static void test_insert()
        {
            var content = new Content()
            {
                title = "标题",
                content = "内容1"
            };
            using (var conn = new SqlConnection("Server=(local);Database=cmsTest;Trusted_Connection=True;"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT into [Content] ");
                sb.Append("( title,[content],status,add_time,modify_time ) ");
                sb.Append("VALUES ");
                sb.Append("( @title,@content,@status,@add_time,@modify_time )");
                var result = conn.Execute(sb.ToString(), content);
                Console.WriteLine($"插入了{result}条数据");
            }
        }



        /// <summary>
        /// 测试一次批量插入两条数据
        /// </summary>
        static void test_mult_insert()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                title = "批量插入标题1",
                content = "批量插入内容1",

            },
               new Content
            {
                title = "批量插入标题2",
                content = "批量插入内容2",

            },
        };
            //Data Source=127.0.0.1;User ID=sa;Password=1;Initial Catalog=Czar.Cms;Pooling=true;Max Pool Size=100;
            using (var conn = new SqlConnection("Server=(local);Database=cmsTest;Trusted_Connection=True;"))
            {
                string sql_insert = @"INSERT INTO [Content]
                (title, [content], status, add_time, modify_time)
VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_insert：插入了{result}条数据！");
            }
        }

        static void test_del()
        {
            var content = new Content()
            {
                id = 2
            };
            using (var conn = new SqlConnection("Server=(local);Database=cmsTest;Trusted_Connection=True;"))
            {
                string sql_del = "DELETE FROM content WHERE id=@id";
                var result = conn.Execute(sql_del, content);
                Console.WriteLine($"test_del：删除了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试一次批量删除两条数据
        /// </summary>
        static void test_mult_del()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                id=3,

            },
               new Content
            {
                id=4,

            },
        };

            using (var conn = new SqlConnection("Server=(local);Database=cmsTest;Trusted_Connection=True;"))
            {
                string sql_insert = @"DELETE FROM [Content]
WHERE   (id = @id)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_del：删除了{result}条数据！");
            }
        }
    }
}
