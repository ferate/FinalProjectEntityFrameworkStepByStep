using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static readonly string ProductAdded = "Ürün eklendi";
        public static readonly string ProductDeleted = "Ürün silinci";
        public static readonly string ProductUpdated = "Ürün güncellendi";
        public static readonly string ProductNameInvalid = "Ürün ismi geçersiz";
        public static readonly string MaintenanceTime = "Sistem Bakımda";
        public static readonly string ProductsListed = "Ürünler Listelendi";
        public static readonly string ProductShowed = "Ürün görüntülendi";
        public static readonly string ProductDetailListed = "Ürün Detayları Listelendi";
        public static readonly string CategoriesListed = "Kategoriler Listelendi";
        public static readonly string CategoryShowed = "Kategoriler görüntülendi";
        public static readonly string CategoryLimitExcede= "Kategori Limiti Aşıldı Yeni Ürün Eklenemiyor";
     
        public static readonly string AuthorizationDenied = "Yetkiniz yok";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
    }
}
