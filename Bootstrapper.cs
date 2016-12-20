using System.IO;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Diagnostics;
using Nancy.Session;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace Galette {

    //=================================================================================================/
    /// <summary>
    /// Nancy の設定をする
    /// </summary>
    //=================================================================================================/
    public class Bootstrapper : DefaultNancyBootstrapper {

        protected override IRootPathProvider RootPathProvider => new RootPathProvider();

        //=============================================================================================/
        /// <summary>
        /// INancyEnvironment の設定
        /// </summary>
        /// <param name="environment"></param>
        //=============================================================================================/
        public override void Configure(INancyEnvironment environment) {
            environment.Diagnostics(
                enabled: true,
                password: "qwerty",
                path: "/_Nancy",
                cookieName: "__custom_cookie",
                slidingTimeout: 30
            );

            environment.Tracing(
                enabled: true,
                displayErrorTraces: true
            );
        }

        //=============================================================================================/
        /// <summary>
        /// アプリケーションパイプラインの設定
        /// Session を有効にし、全てのリクエスト処理の前にユーザー情報の保存をする
        /// </summary>
        /// <param name="container"></param>
        /// <param name="pipelines"></param>
        //=============================================================================================/
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines) {
            base.ApplicationStartup(container, pipelines);
            CookieBasedSessions.Enable(pipelines);

            //pipelines.BeforeRequest += setUserSession;
        }

/*
        //=============================================================================================/
        /// <summary>
        /// Azure が取得したメールアドレスを Session に保存する
        /// </summary>
        /// <param name="context"></param>
        /// <returns>リクエストを中断する場合は Response, そうでなければ null</returns>
        //=============================================================================================/
        private Response setUserSession(NancyContext context) {
            var headers = context.Request.Headers;
            var email = string.Join("", headers["X-MS-CLIENT-PRINCIPAL-NAME"]);

            // Donuts 社員じゃないならはじく
            if(context.IsProduction() && !email.EndsWith("donuts.ne.jp")) {
                return HttpStatusCode.Forbidden;
            }

            // セッション作成時に Google の情報を更新する
            if(context.IsProduction() && context.Request.Session["UserEmail"] == null) {
                var token = string.Join("", headers["X-MS-TOKEN-GOOGLE-ACCESS-TOKEN"]);
                var person = GoogleApiHelper.GetPeople(token);

                // Google の情報でユーザーを更新する
                using(var dbContext = new UserDbContext()) {
                    var user = dbContext.Users.SingleOrDefault(u => u.Email == email);
                    if(person.IsValid() && user != null) {
                        user.FirstName = person.Name.GivenName;
                        user.LastName = person.Name.FamilyName;
                        user.ProfileImageUrl = person.Image.IsDefault ?
                            "/Content/img/icons/faceicon_a_08.png" :
                            person.Image.Url;
                        dbContext.SaveChanges();
                    }
                }
            }

            context.Request.Session["UserEmail"] = email;
            return null;
        }
*/
    }

    public class RootPathProvider : IRootPathProvider
    {
        public string GetRootPath() {
            return Directory.GetCurrentDirectory();
        }
    }
}