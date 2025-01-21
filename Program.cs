using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriYapilariOdev_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Linear Probing için hash tablosu
            HashTablosu linearProbingTablo = new HashTablosu();

            // 100 rastgele anahtar üret ve Linear Probing ile yerleştir
            for (int i = 0; i < 100; i++)
            {
                int anahtar = HashTablosu.RandomAnahtar();
                linearProbingTablo.LinearProbingEkle(anahtar);
            }

            // Linear Probing ile yerleştirilen hash tablosunu yazdır
            Console.WriteLine("### Linear Probing Çakışma Çözümü ###");
            linearProbingTablo.yazdir();

            Console.WriteLine(" ");

            // Quadratic Probing için hash tablosu
            HashTablosu quadraticProbingTablo = new HashTablosu();

            // 100 rastgele anahtar üret ve Quadratic Probing ile yerleştir
            for (int i = 0; i < 100; i++)
            {
                int anahtar = HashTablosu.RandomAnahtar();
                quadraticProbingTablo.QuadraticProbingEkle(anahtar);
            }
            Console.WriteLine(" ");

            // Quadratic Probing ile yerleştirilen hash tablosunu yazdır
            Console.WriteLine("### Quadratic Probing Çakışma Çözümü ###");
            quadraticProbingTablo.yazdir();

            Console.ReadLine();
        }


        
    }

    class HashTablosu
    {
        private  int boyut = 100;  // Hash tablosunun boyutu
        private int[] tablo;  // Hash tablosu dizisi

        //  Hash tablosunu başlatır ve tüm hücreleri -1 ile doldurur (boş hücreler)
        public HashTablosu()
        {
            tablo = new int[boyut];

            // Dizi elemanlarını -1 ile dolduruyoruz (boş hücreleri simgeliyor)
            for (int i = 0; i < boyut; i++)
            {
                tablo[i] = -1;  // -1, boş hücreyi simgeliyor
            }
        }

        // Hash fonksiyonu (Division Method) 
        private int HashFonksiyonu(int anahtar)
        {
            return anahtar % boyut;  // anahtarın modülünü alarak tablonun indeksini hesaplar
        }

        // Linear Probing ile anahtar ekleme
        public void LinearProbingEkle(int anahtar)
        {
            int index = HashFonksiyonu(anahtar);  // Anahtarı hash fonksiyonu ile indeksle
            int baslangıcIndex = index;  // Başlangıç indeksi kaydet

            // Linear Probing: Eğer mevcut hücre doluysa, bir sonraki hücreye bak
            while (tablo[index] != -1)
            {
                index = (index + 1) % boyut;  // Bir sonraki hücreye geç
                if (index == baslangıcIndex)  // Eğer tablo doluysa, yerleştirme yapılmaz
                {
                    Console.WriteLine("Tablo doldu, ekleme yapılacak yer yok.");
                    return;
                }
            }

            tablo[index] = anahtar;  // Boş hücre bulundu, anahtarı ekle
        }

        // Quadratic Probing ile anahtar ekleme
        public void QuadraticProbingEkle(int anahtar)
        {
            int index = HashFonksiyonu(anahtar);  // Anahtarı hash fonksiyonu ile indeksle
            int i = 1;  // Adım sayısı
            int baslangıcIndex = index;  // Başlangıç indeksi kaydet

            // Quadratic Probing: Çakışma durumunda, kareli artışlarla ilerle
            while (tablo[index] != -1)
            {
                index = (index + i * i) % boyut;  // Kareli adımla ilerle
                i++;  // Adımı artır
                if (index == baslangıcIndex)  // Eğer tablo doluysa, yerleştirme yapılmaz
                {
                    Console.WriteLine("Tablo doldu, ekleme yapılacak yer yok.");
                    return;
                }
            }

            tablo[index] = anahtar;  // Boş hücre bulundu, anahtarı ekle
        }

        // Hash tablosunu yazdırma
        public void yazdir()
        {
            Console.WriteLine("Hash Tablosu:");
            for (int i = 0; i < boyut; i++)
            {
                if (tablo[i] != -1)
                    Console.WriteLine($"Index {i}: {tablo[i]}");
                else
                    Console.WriteLine($"Index {i}: (empty)");  // Boş hücreyi belirt
            }
        }

        // Anahtarları rastgele üretme
        public static int RandomAnahtar()
        {
            Random rand = new Random();
            return rand.Next(1, 1000);  // 1 ile 999 arasında rastgele bir anahtar üret
        }
    }
  
}
