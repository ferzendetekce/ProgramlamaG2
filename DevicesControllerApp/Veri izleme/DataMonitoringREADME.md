Rehabilitasyon Veri İzleme Modülü

Bu proje, rehabilitasyon cihazlarından gelen basınç verilerini simüle ederek gerçek zamanlı grafikler üzerinden takip etmeyi sağlar. Uygulama, WinForms tabanlıdır ve cihazdan gelen verileri chart bileşenleriyle gösterir. Gerçek cihaz bağlantısı olmadan test amacıyla sahte veri üretimi (simülasyon) yapılmaktadır.

Proje Yapısı

DevicesControllerApp/
│
└── Veri_izleme/
├── NewDataEventArgs.cs      → Veri paket yapısı
├── DeviceCommunication.cs → Arka planda sahte sensör verisi üreten sınıf
└── DataMonitoring.cs      → Grafiksel izleme arayüzü (UserControl)

Bileşenler

1️- NewDataEventArgs.cs
Cihazdan gelen tüm sensör verilerini tek bir paket halinde taşır. Bu sınıf, DeviceCommunication tarafından üretilip DataMonitoring arayüzüne event yoluyla aktarılır.

Özellikler:

-SagTabanData (List<double>): Sağ taban sensörleri

-SolTabanData (List<double>): Sol taban sensörleri

-SagTopukData (List<double>): Sağ topuk sensörleri

-SolTopukData (List<double>): Sol topuk sensörleri

-AgirlikDengesi (double): Toplam ağırlık değeri

-IsStepDetected (bool): Adım algılandı mı (true/false)

2️- DeviceCommunication.cs
Cihazdan veri geliyormuş gibi davranan arka plan simülasyon sınıfıdır.

Görevleri:

-Her 1 saniyede 20 veri paketi üretir (TargetFPS = 20).

-100 noktalık sensör verisi oluşturur (PointCount = 100).

-Üretilen veriyi OnNewData eventiyle UI katmanına gönderir.

Kullanım:
var device = new DeviceCommunication();
device.OnNewData += Device_OnNewData;
device.Start();

3️- DataMonitoring.cs
Kullanıcı arayüzünü ve grafiksel izlemeyi yöneten WinForms UserControl bileşenidir. Arka planda çalışan DeviceCommunication sınıfından gelen verileri alır, işler ve grafiklere uygular.

Gösterilen Grafikler:

-Sağ Ayak Tabanı

-Sol Ayak Tabanı

-Sağ Ayak Topuğu

-Sol Ayak Topuğu

-Ağırlık Dengesi (bar chart)

Çalışma Akışı:

DataMonitoring_Load() çağrılır → grafikler ayarlanır.

DeviceCommunication.Start() başlatılır.

Her veri paketinde Device_OnNewData() tetiklenir.

Adım algılandığında ProcessData() çağrılır → grafikler güncellenir.

“Örnek Al” butonu kırmızı veri serisini oluşturur.

“Verileri Dışa Aktar” CSV dosyasına yazar.

Önemli Metotlar:

-SetupCharts(): Grafiklerin ilk kurulumunu yapar.

-ProcessData(NewDataEventArgs e): Yeni verileri grafiklere işler.

-UpdateScatter(Chart, List<double>): Grafik noktalarını günceller.

-btnFreeze_Click(): Örnek veri (kırmızı) oluşturma / temizleme.

-btnExportData_Click(): CSV dışa aktarım işlemini yönetir.

-ExportChartDataToCsv(): Grafik verilerini CSV formatına dönüştürür.

-CopyBlueToRed(): Canlı veriyi kırmızı seriye kopyalar.

-StopDeviceCommunication(): Arka plan veri üretimini durdurur.