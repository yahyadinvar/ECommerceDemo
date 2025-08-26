# ECommerceDemo
Özellikler ve Kullanılan Tasarım Desenleri
- Clean Architecture - DDD : Proje katmanlı ve bağımsız bir yapı ile tasarlandı.
- Minimal API: Controller yerine minimal API endpointleri kullanıldı.
- CQRS ve MediatR : Command ve Query ayrımı yapıldı, MediatR ile işlemler yönetildi.
- Validation ve Logging Behaviors: FluentValidation ile request validation, logging behavior ile loglama sağlandı.
- Global Exception Handling: Hatalar merkezi olarak yakalanıp yönetildi.
- AutoMapper: Entity -> DTO mapping işlemleri için kullanıldı.
- Outbox Pattern: Veri tutarlılığı için event’ler Outbox tablosunda saklandı.
- MassTransit + RabbitMQ: Asenkron mesajlaşma ve event publishing için kullanıldı.
- Domain Events (EventBus): Domain katmanında oluşan event publish edildi.
- JWT Token Authentication: Kullanıcı kimlik doğrulama için JSON Web Token kullanıldı.
- IOptions Pattern: AppSettings’deki konfigürasyon verilerini güvenli ve düzenli okumak için kullanıldı.
- Result Pattern: Sonuçları yönetmek için kullanıldı.
- Repository ve Unit of Work Pattern: Veri erişim katmanı için generic repository ve unit of work kullanıldı.
- SQL Server + Code First Migrations: Database yapısı migration ile otomatik oluşturuldu.
- Serilog: Loglama için kullanıldı.
- Redis-Docker : Cache için redis kullanıldı, docker'da ayağa kaldırıldı.

  *** Uygulama Kullanırken Dikkat Edilmesi Gerekenler
  - Uygulama içerisindeki appsettings.development.json dosyasındaki sql server ayarlarını kendi bilgilerinize göre ayarlayınız.
  - API ve Worker (consumer simulasyonu için) katmanı multiple seçilerek uygulama ayağa kaldırıldığında migration'lar otomatik çalışacak ve db seçtiğiniz adreste oluşacaktır.
  - Redis configurasyonu için, önerilen yöntem docker kurulmalı sonrasında; 'docker run -d --name my_redis -p 6379:6379 redis' komutu çalıştırılıp default portunda ayağa kaldırılmalı. Demo uygulamada cache mekanizmasının düzgün çalışması için yeterli olacaktır.
  - Sonrasında api arayüzden https://localhost:7038/swagger/index.html'e giderek Register endpoint'i ile uygulamaya kayıt olunuz.
  - Daha sonra kayıt olduğunuz email adresi ve şifreniz ile Login endpoint'ini kullanarak sistemden token bilgisini alınız.
  - Bu token bilgisi ile sağ üstteki girişten Bearer token girişi yapınız.
  - Bu işlemler sonucunda uygulamaya giriş yapmış ve yetkilendirme almış olacağınızdan kilitli olan diğer endpoint'leri de test edebilirsiniz.
