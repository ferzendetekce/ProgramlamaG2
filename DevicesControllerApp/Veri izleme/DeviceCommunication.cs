using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevicesControllerApp.Veri_izleme
{
    // Arka planda sahte sensör verisi üreten ve bunu UI'a (arayüze) gönderen sınıf
    internal class DeviceCommunication
    {
        // --- EVENT ---
        // UI'a yeni veri geldiğini bildiren event. Arayüz bu event'i dinler
        public event EventHandler<NewDataEventArgs> OnNewData;

        // --- DEĞİŞKENLER ---
        private CancellationTokenSource cts;       // Arka plan görevini durdurmak için kullanılan sinyal
        private readonly Random rnd = new Random();      // Rastgele veri üretmek için
        private const int PointCount = 100;              // Her ayak grafiği için üretilecek veri noktası sayısı
        private const int TargetFPS = 20;                // Saniyede kaç veri paketi üretileceği (hedef kare hızı)

        // --- KONTROL METOTLARI ---

        // Veri üretme döngüsünü arka planda (UI'ı dondurmadan) başlatır
        public void Start()
        {
            cts = new CancellationTokenSource();
            Task.Run(() => GenerateDataLoop(cts.Token));
        }

        // Arka plandaki veri üretme döngüsüne durma sinyali gönderir
        public void Stop()
        {
            cts?.Cancel();
        }

        // --- ANA VERİ DÖNGÜSÜ ---

        // Durdurma sinyali gelene kadar sürekli çalışan ana metot
        private async Task GenerateDataLoop(CancellationToken token)
        {
            int stepCounter = 0; // Adım simülasyonu için sayaç
            while (!token.IsCancellationRequested) // Durdurma sinyali gelmediği sürece devam et
            {
                // 1. Veri üretme (Simülasyon)
                var sagTaban = new List<double>();
                var solTaban = new List<double>();
                var sagTopuk = new List<double>();
                var solTopuk = new List<double>();

                for (int i = 0; i < PointCount; i++)
                {
                    // Her sensör bölgesi için rastgele değerler oluştur
                    sagTaban.Add(rnd.Next(5, 80));
                    solTaban.Add(rnd.Next(5, 75));
                    sagTopuk.Add(rnd.Next(10, 90));
                    solTopuk.Add(rnd.Next(10, 85));
                }

                // Ağırlık için sinüs dalgası kullanarak yumuşak bir geçiş simüle et
                double agirlik = 75 + 15 * Math.Sin(DateTime.Now.Millisecond / 500.0);

                // 2. Adım Algılama (Simülasyon)
                stepCounter++;
                // Her 25 veri paketinde bir, bir "adım" algılandığını varsay
                bool isStep = (stepCounter % 25 == 0);

                // 3. Veri Paketini Oluştur
                var eventArgs = new NewDataEventArgs
                {
                    SagTabanData = sagTaban,
                    SolTabanData = solTaban,
                    SagTopukData = sagTopuk,
                    SolTopukData = solTopuk,
                    AgirlikDengesi = agirlik,
                    IsStepDetected = isStep
                };

                // 4. Event'i Tetikle
                // "Yeni veri var!" diye UI'a haber ver ve veri paketini gönder
                OnNewData?.Invoke(this, eventArgs);

                // 5. Bekleme
                // Programı dondurmadan, hedeflenen FPS'ye ulaşmak için kısa bir süre bekle
                await Task.Delay(1000 / TargetFPS, token);
            }
        }
    }
}