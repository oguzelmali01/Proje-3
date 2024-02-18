using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace proje3
{
    public class Program
    {
        static void Main(string[] args)
        {
            //1.SORU
            //A ŞIKKI
            Tree Agac = new Tree();
            List<UM_Alanı> umAlanListesi = UM_Alanı_Listesi_Bulma();
            foreach (var umAlan in umAlanListesi)
            {
                Agac.insert(umAlan);
            }

            //B ŞIKKI
            //DerinlikveDugumSayısıBulma(Agac.getRoot());
            //Agac.preOrder(Agac.getRoot());

            //C ŞIKKI
            //List<string> ikiharflistem = new List<string>();
            //ikiharfalma(Agac.getRoot(), "g", "L", ikiharflistem);
            //Console.WriteLine("Girilen harfler arsındaki UM alan adları aşağıdaki gibidir.");
            //foreach (var bulunan in ikiharflistem)
            //{
            //    Console.WriteLine(bulunan);
            //}
            //D şıkkı
            //List<UM_Alanı> siraliAlanlar = new List<UM_Alanı>();
            //Agac.inOrder(Agac.getRoot(), siraliAlanlar);
            //int ortaIndex = siraliAlanlar.Count / 2;
            //UM_Alanı yeniAgacKok = siraliAlanlar[ortaIndex];
            //// Yeni ağaç oluşturma ve kökü ayarlama
            //Tree yeniAgac = new Tree();
            //yeniAgac.insert(yeniAgacKok);
            //DengeliAgacOlustur(siraliAlanlar, yeniAgac, 0, ortaIndex - 1);
            //DengeliAgacOlustur(siraliAlanlar, yeniAgac, ortaIndex + 1, siraliAlanlar.Count - 1);
            //yeniAgac.inOrder(yeniAgac.getRoot());            
            //2. SORU 
            //A ŞIKKI
            //Dictionary<string, UM_Alanı> hashTable = new Dictionary<string, UM_Alanı>();

            //foreach (var umAlan in umAlanListesi)
            //{
            //    // Alan Adı'na göre Hash Table'a ekleme
            //    hashTable.Add(umAlan.Alan_Adi, umAlan);
            //}
            //B ŞIKKI
            //Hashtabledegistirme(hashTable, "Divrigi Ulu Camii ve Darussifasi", "ADANA MERKEZ");
            //3.SORU
            Heap HeapListem = new Heap(umAlanListesi.Count);
            foreach (var item in umAlanListesi)
            {
                HeapListem.insert(item);
            }
            //B ŞIKKI
            //HeapListem.DisplayHeap();
            //C ŞIKKI
            //HeapAlfabetik(HeapListem);
        }
        static List<UM_Alanı> UM_Alanı_Listesi_Bulma()
        {
            List<UM_Alanı> umAlanListesi = new List<UM_Alanı>();

            string cityFilePath = "veri.txt";

            using (StreamReader cityFile = new StreamReader(cityFilePath))
            {
                string row;
                while ((row = cityFile.ReadLine()) != null)
                {
                    string[] parcalar = row.Split('/');

                    // İlgili parçaları kullanarak UM_Alanı nesnesi oluşturma
                    string alanAdi = parcalar[0].Trim();
                    string ilAdi = parcalar[1].Trim();
                    string[] ilAdiArray = ilAdi.Split('&');
                    int ilanYili = int.Parse(parcalar[2].Trim());
                    string paragraf = parcalar[3].Trim();
                    string[] AllParagaf = paragraf.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> paragrafListesi = AllParagaf.ToList();



                    UM_Alanı umAlan = new UM_Alanı(alanAdi, ilAdiArray, ilanYili,paragrafListesi);

                    // Oluşturulan UM_Alanı nesnesini listeye ekleme
                    umAlanListesi.Add(umAlan);
                }
            }
            return umAlanListesi;
        }
        static void DerinlikveDugumSayısıBulma(TreeNode root)
        {
            // Eğer kök düğüm null ise, ağacın derinliği 0'dır.
            if (root == null)
            {
                Console.WriteLine("Ağaç boş.");
                return;
            }

            // Kuyruk (Queue) veri yapısı oluşturuluyor ve kök düğüm kuyruğa ekleniyor.
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            // Derinlik sayacı başlangıçta 0 olarak ayarlanıyor.
            int derinlik = 0;
            int dugumSayisi = 0;

            // Kuyruk boşalana kadar devam eden bir döngü.
            while (queue.Count > 0)
            {
                // Her seviyede bulunan düğüm sayısı alınıyor.
                int levelSize = queue.Count;

                // Her seviyede bulunan düğümleri işleyen iç içe döngü.
                for (int i = 0; i < levelSize; i++)
                {
                    // Kuyruktan bir düğüm çıkarılıyor.
                    TreeNode currentNode = queue.Dequeue();
                    dugumSayisi++;

                    // Eğer sol alt ağaçta düğüm varsa, kuyruğa ekleniyor.
                    if (currentNode.leftChild != null)
                    {
                        queue.Enqueue(currentNode.leftChild);
                    }

                    // Eğer sağ alt ağaçta düğüm varsa, kuyruğa ekleniyor.
                    if (currentNode.rightChild != null)
                    {
                        queue.Enqueue(currentNode.rightChild);
                    }
                }

                // Seviye tamamlandığında derinlik bir artırılıyor.
                derinlik++;
            }

           
            Console.WriteLine("Ağacın Derinliği: " + (derinlik - 1));
            Console.WriteLine($"Ağaçtaki Düğüm Sayısı: {dugumSayisi}");
            //dengeli ağaç olsaydı bulunacak derinlik
            int dengelininderinlik = (int)Math.Floor(Math.Log(dugumSayisi + 1, 2));
            Console.WriteLine($"Dengeli ağaç olsaydı derinliği:{dengelininderinlik}");
        }
        static void ikiharfalma(TreeNode root, string ilkharf, string sonharf, List<string> ikiharflistem)
        {
            ilkharf = ilkharf.ToUpper();
            sonharf = sonharf.ToUpper();
            //eğer iki harfin sırası ters olursa
            if (ilkharf.CompareTo(sonharf) > 0)
            {
                string temp = ilkharf;
                ilkharf = sonharf;
                sonharf = temp; 
            }

            if (root != null)
            {
                if (ilkharf[0] < root.data.Alan_Adi[0] && root.data.Alan_Adi[0] < sonharf[0])
                {                     
                    ikiharflistem.Add(root.data.Alan_Adi);
                }                
                ikiharfalma(root.leftChild, ilkharf, sonharf,ikiharflistem );
                ikiharfalma(root.rightChild, ilkharf, sonharf, ikiharflistem);
            }
        }
        static void DengeliAgacOlustur(List<UM_Alanı> siraliAlanlar, Tree agac, int baslangicIndex, int bitisIndex)
        {
            if (baslangicIndex <= bitisIndex)
            {
                // İkinci yarıdan bir elemanı seçme
                int ortaIndex = (baslangicIndex + bitisIndex) / 2;
                UM_Alanı yeniAgacKok = siraliAlanlar[ortaIndex];

                // Yeni düğümü ekleme
                agac.insert(yeniAgacKok);

                // özyineleme
                DengeliAgacOlustur(siraliAlanlar, agac, baslangicIndex, ortaIndex - 1);
                DengeliAgacOlustur(siraliAlanlar, agac, ortaIndex + 1, bitisIndex);
            }
        }
        static void Hashtabledegistirme(Dictionary<string, UM_Alanı> hashTable, string ilkad,string sonad)
        {
            if (hashTable.TryGetValue(ilkad, out UM_Alanı bulunanUmAlan))
            {
                Console.WriteLine($"Eski Alan Adı: {ilkad}");
                hashTable.Remove(ilkad);
                bulunanUmAlan.Alan_Adi = sonad;
                hashTable.Add(bulunanUmAlan.Alan_Adi, bulunanUmAlan);
                Console.WriteLine($"UM_Alanı değişti. Yeni Alan Adı: {bulunanUmAlan.Alan_Adi}");
            }
            else
            {
                Console.WriteLine("Belirtilen Alan Adı'na sahip UM_Alanı bulunamadı.");
            }

        }
        static void HeapAlfabetik(Heap HeapListem) 
        {
            Console.WriteLine("Alfabetik olarak en önce gelen 3 UM alanı:");
            int sayi = 3;
            for (int i = 0; i < sayi; i++)
            {
                if (HeapListem.isEmpty())
                {
                    Console.WriteLine("Heap boş");
                }
                UM_Alanı eleman = HeapListem.remove();
                Console.WriteLine($"{eleman.İl_Adı[0]},{eleman.Alan_Adi}");

            }
        }
        static void BubbleSort(int[] dizi)
        {
            int n = dizi.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (dizi[j] > dizi[j + 1])
                    {
                        int temp = dizi[j];
                        dizi[j] = dizi[j + 1];
                        dizi[j + 1] = temp;
                    }
                }
            }
        }
    }
    class TreeNode    // Düğüm Sınıfı
    {
        public UM_Alanı data;
        public TreeNode leftChild;
        public TreeNode rightChild;
        public void kokdenyazdırma() {
            Console.WriteLine($"Alan Adı: {data.Alan_Adi}");
            Console.WriteLine($"İl Adı: {string.Join(", ", data.İl_Adı)}");
            Console.WriteLine($"İlan Yılı: {data.İlan_Yılı}");
            Console.WriteLine("Kelimeler:");
            foreach (var kelime in data.Paragraf)
            {
                Console.Write(kelime + " , ");
            }
            Console.WriteLine();
        }
        
        
    }
    class Tree    // Agaç Sınıfı
    {
        public TreeNode root;
        

        public Tree() { 
            root = null;
        }

        public TreeNode getRoot()
        { return root; }

        // Agacın preOrder Dolasılması
        public void preOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                localRoot.kokdenyazdırma();
                preOrder(localRoot.leftChild);
                preOrder(localRoot.rightChild);
            }
        }
        

        // Agacın inOrder Dolasılması
        public void inOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                inOrder(localRoot.leftChild);
                
                localRoot.kokdenyazdırma();
                
                inOrder(localRoot.rightChild);
            }
        }
        public void inOrder(TreeNode localRoot, List<UM_Alanı> siraliAlanlar)
        {
            if (localRoot != null)
            {
                inOrder(localRoot.leftChild, siraliAlanlar);
                siraliAlanlar.Add(localRoot.data); // Alan adına göre sıralı diziye ekleme
                inOrder(localRoot.rightChild, siraliAlanlar);
            }
        }

        // Agacın postOrder Dolasılması
        public void postOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                postOrder(localRoot.leftChild);
                postOrder(localRoot.rightChild);
                localRoot.kokdenyazdırma();
            }
        }

        // Agaca bir dügüm eklemeyi saglayan metot
        public void insert(UM_Alanı newdata)
        {
            TreeNode newNode = new TreeNode();
            newNode.data = newdata;
            if (root == null)
                root = newNode;
            else
            {
                TreeNode current = root;
                TreeNode parent;
                while (true)
                {
                    parent = current;
                    if (string.Compare(newdata.Alan_Adi, current.data.Alan_Adi) < 0)
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = newNode;
                            return;
                        }
                    }
                    else
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = newNode;
                            return;
                        }
                    }
                } 
            } 
        } 

    }
    class UM_Alanı
    {
        // Özellikler (Properties)
        public string Alan_Adi { get; set; }
        public string[] İl_Adı { get; set; }
        public int İlan_Yılı { get; set; }
        public List<string> Paragraf {  get; set; }

        // Kurucu Metot (Constructor)
        public UM_Alanı(string Alan_Adi, string[] İl_Adı, int İlan_Yılı, List<string> Paragraf)
        {
            this.Alan_Adi = Alan_Adi;
            this.İl_Adı = İl_Adı;
            this.İlan_Yılı = İlan_Yılı;
            this.Paragraf = Paragraf;
        }
        
    }
    class Node
    {
        private UM_Alanı iData;                           
        public Node(UM_Alanı key) // constructor
        { iData = key; }        
        public UM_Alanı getKey()
        { return iData; }
        public void setKey(UM_Alanı id)
        { iData = id; }
    } 
    class Heap
    {
        private Node[] heapArray;
        private int maxSize; // size of array
        private int currentSize; // number of nodes in array                                 
        public Heap(int mx) // constructor
        {
            maxSize = mx;
            currentSize = 0;
            heapArray = new Node[maxSize]; // create array
        }       
        public bool isEmpty()
        { return currentSize == 0; }
        public bool insert(UM_Alanı key)
        {
            if (currentSize == maxSize)
                return false;
            Node newNode = new Node(key);
            heapArray[currentSize] = newNode;
            trickleUp(currentSize++);
            return true;
        }
        public void trickleUp(int index)
        {
            int parent = (index - 1) / 2;
            Node bottom = heapArray[index];
            //Array.Sort(heapArray[parent].getKey().İl_Adı);
            //Array.Sort(bottom.getKey().İl_Adı);
            while (index > 0 && heapArray[parent].getKey().İl_Adı[0].CompareTo(bottom.getKey().İl_Adı[0]) > 0)
            {
                heapArray[index] = heapArray[parent];
                index = parent;
                parent = (parent - 1) / 2;
                //Array.Sort(heapArray[parent].getKey().İl_Adı);
                
            }
            heapArray[index] = bottom;
        }
        public UM_Alanı remove() 
        { // (assumes non-empty list)
            Node root = heapArray[0];
            heapArray[0] = heapArray[--currentSize];
            trickleDown(0);
            return root.getKey();
        }
        public void trickleDown(int index)
        {
            int minChild;
            Node top = heapArray[index]; // save root
            while (index < currentSize / 2) // while node has at
            { // least one child,
                int leftChild = 2 * index + 1;
                int rightChild = leftChild + 1;
                // find larger child
                if (rightChild < currentSize && heapArray[leftChild].getKey().İl_Adı[0].CompareTo(heapArray[rightChild].getKey().İl_Adı[0])>0)                    
                    minChild = rightChild;
                else
                    minChild = leftChild;
                // top >= largerChild?
                if (top.getKey().İl_Adı[0].CompareTo(heapArray[minChild].getKey().İl_Adı[0])<=0)
                    break;              
                heapArray[index] = heapArray[minChild];
                index = minChild;
            }
            heapArray[index] = top;
        }
        //public bool change(int index, int newValue)
        //{
        //    if (index < 0 || index >= currentSize)
        //        return false;
        //    int oldValue = heapArray[index].getKey(); // remember old
        //    heapArray[index].setKey(newValue); // change to new
        //    if (oldValue < newValue) // if raised,
        //        trickleUp(index); // trickle it up
        //    else // if lowered,
        //        trickleDown(index); // trickle it down
        //    return true;
        //} // end change()
        public void DisplayHeap()
        {
            Console.WriteLine("heapArray: "); 
            for (int m = 0; m < currentSize; m++)
            {
                if (heapArray[m] != null)
                    Console.WriteLine(string.Join(" ", heapArray[m].getKey().İl_Adı) +" / "+ heapArray[m].getKey().Alan_Adi);
                else
                    Console.Write("-- ");
            }
            Console.WriteLine();

            
        } // end DisplayHeap()
    }    
}
