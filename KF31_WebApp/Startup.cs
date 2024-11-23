using KF31_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KF31_WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // サービスの設定
        public void ConfigureServices(IServiceCollection services)
        {
            // MVCサービスを追加
            services.AddControllersWithViews();

            // DbContextをSQL Serverで設定
            services.AddDbContext<KF31_LliM5_DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("KF31DBConnectionString")));

            // セッションの設定
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // セッションの有効期限
                options.Cookie.HttpOnly = true; // Cookieのセキュリティを強化
                options.Cookie.IsEssential = true; // 必須Cookieとしてマーク
            });

            // Cookie認証の設定
            services.AddAuthentication("Cookie").AddCookie("Cookie", options =>
            {
                options.LoginPath = "/Login/Login"; // ログインページのパス
                options.LogoutPath = "/Login/Logout"; // ログアウトページのパス
                options.AccessDeniedPath = "/Login/AccessDenied"; // アクセス拒否時のパス
                options.ExpireTimeSpan = TimeSpan.FromHours(1); // Cookieの有効期限（1時間）
                options.SlidingExpiration = true; // アクティビティがある場合はCookieを更新
            });
        }

        // ミドルウェアの設定
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 開発環境でのエラーページを表示
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // 本番環境でのエラー処理
                app.UseExceptionHandler("/Error/Error");
                app.UseHsts();
            }

            app.UseStaticFiles(); // 静的ファイルを使用

            app.UseRouting(); // ルーティングの処理

            // 認証用ミドルウェアを追加
            app.UseAuthentication();
            app.UseAuthorization();

            // セッション用ミドルウェアを追加
            app.UseSession();

            // デフォルトのルーティング設定
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
