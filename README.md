# SeturContacts

Aslında niyetim bu assesment projesini Clientlerı ile birlikte full stack olarak geliştiirp komple dockerize edip publish ederek cnalı demo sunömaktı ancak mevcut iş yoğunluğum ve sağlık sorunlaırm nedeniyle deadline'ı daha fazla ertelememek adına kafamdaki projeyi tam oalrak gerçekleştiremedim fakat ek bir süre tanınırsa ve istenirse projeye devam edebilirim.

# Genel Bilgi
Projede 3 adet mikro servis kullandım 
2 adet mongoDB veri tabanı docker imajı olarak çalışıyor.
Mass Transit ile RabbitMQ kullanıyorum
Identity Server projeye dahi ledildi ancak configure etmedim, zamanım olursa token exchange ve identity servisi ile endpointler de kullanıcıya göre işlem yapacak şekilde düzenleme yapabilirim.
Rapor oluşturan mikro serviste de eventual consistnecy kullanarak contact mikro servisine olan bağlılığı ortadan kaldırıp standalone çalışacak şekilde kurgulamak istiyorum. 
eğer ilk mikroservis çökse bile rapor oluşturm işleminde aksama olmasını istemem.
Tüm endpointler swagger ile test edildiği için UnitTest yazma gereksinimi duymadım

# Services
Her bir servis küçük bir web api projesi.

ilk mikro servis aşağıdaki

--------------------------
*Rehberde kişi oluşturma
*Rehberde kişi kaldırma
*Rehberdeki kişiye iletişim bilgisi ekleme
*Rehberdeki kişiden iletişim bilgisi kaldırma
*Rehberdeki kişilerin listelenmesi
*Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi

--------------------------

görevlerini yerine getirirken 2. mikro servis 

--------------------------

*Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi
*Sistemin oluşturduğu raporların listelenmesi
*Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi

--------------------------
görevlerinden sorumlu. 2. mikro servis için oluşturulan raporlar içerisinde ilk mikro servisten aldığı contact bilgilerini barındırıyor ancak raporları veri tabanından okutmak yerine static path aktif edip html docx veya xlsx formatında servis etme niyetindeyim

