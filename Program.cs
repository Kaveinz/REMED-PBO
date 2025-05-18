using System;



class Program
{
    static void Main(string[] args)
    {
        Perpustakaan perpustakaan = new Perpustakaan();

        perpustakaan.TambahBuku(new BukuFiksi("The Hobbit", "J.R.R. Tolkien", 1937));
        perpustakaan.TambahBuku(new Non_Fiksi("Sapiens", "Yuval Noah Harari", 2011));
        perpustakaan.TambahBuku(new Majalah("National Geographic", "Various", 2023));

        while (true)
        {
            Console.WriteLine("\nManajemen Perpustakaan");
            Console.WriteLine("1.Tambah buku");
            Console.WriteLine("2. Perbarui buku");
            Console.WriteLine("3. Tampilkan semua buku");
            Console.WriteLine("4. pinjam buku");
            Console.WriteLine("5. kembalikan buku");
            Console.WriteLine("6. tampilkan buku yang dipinjam");
            Console.WriteLine("7. keluar");
            Console.Write("mau pilih yang mana: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter book type (1: Fiction, 2: Non-Fiction, 3: Magazine): ");
                    string type = Console.ReadLine();
                    Console.Write("Enter title: ");
                    string judul = Console.ReadLine();
                    Console.Write("Enter author: ");
                    string penulis = Console.ReadLine();
                    Console.Write("Enter publication year: ");
                    int tahun_terbit = int.Parse(Console.ReadLine());

                    if (type == "1")
                    {
                        perpustakaan.TambahBuku(new BukuFiksi(judul, penulis, tahun_terbit));
                    }
                    else if (type == "2")
                    {
                        perpustakaan.TambahBuku(new Non_Fiksi(judul, penulis, tahun_terbit));
                    }
                    else if (type == "3")
                    {
                        
                        perpustakaan.TambahBuku(new Majalah(judul, penulis, tahun_terbit));
                    }
                    break;

                case "2":
                    Console.Write("Perbarui Buku: ");
                    string judullama = Console.ReadLine();
                    Console.Write("judul baru: ");
                    string judulbaru = Console.ReadLine();
                    Console.Write("penulis baru: ");
                    string penulisbaru = Console.ReadLine();
                    Console.Write("tahun terbit baru: ");
                    int tahunbaru = int.Parse(Console.ReadLine());
                    perpustakaan.PerbaruiBuku(judullama, judulbaru, penulisbaru, tahunbaru);
                    break;

                case "3":
                    perpustakaan.TampilkanSemuaBuku();
                    break;

                case "4":
                    Console.Write("masukkan buku yang mau dipinjam: ");
                    string judulyangdipijam = Console.ReadLine();
                    perpustakaan.BukuDipinjam(judulyangdipijam);
                    break;

                case "5":
                    Console.Write("judul buku yang dibalikin: ");
                    string judulyangdibalikin = Console.ReadLine();
                    perpustakaan.KembalikanBuku(judulyangdibalikin);
                    break;

                case "6":
                    perpustakaan.TampilkanBukuDipinjam();
                    break;

                case "7":
                    return;

                default:
                    Console.WriteLine("gada opsi.");
                    break;
            }
        }
    }
}



interface Tentang_buku
{
    void InfoBuku();
    string Apa_Judulnya();
}


abstract class Buku
{
    private string judul;
    private string penulis;
    private int tahun_terbit;
    private bool dipinjam;

    protected Buku(string judul, string penulis, int tahun_terbit)
    {
        this.judul = judul;
        this.penulis = penulis;
        this.tahun_terbit = tahun_terbit;
        this.dipinjam = false;
    }
    public string Judul
    {
        get => judul;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Judul jangan kosong ya.");
            judul = value;
        }
    }

    public string Penulis
    {
        get => penulis;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nama penulis tidak boleh kosong.");
            penulis = value;
        }
    }


    public int Tahun_terbit
    {
        get => tahun_terbit;
        set => tahun_terbit = value;
    }

    public bool Dipinjam
    {
        get => dipinjam;
        set => dipinjam = value;
    }

 
    public abstract void InfoBuku();
}

class BukuFiksi : Buku, Tentang_buku
{

    public BukuFiksi(string judul, string penulis, int tahun_terbit)
        : base(judul, penulis, tahun_terbit)
    {
    }

    public override void InfoBuku()
    {
        Console.WriteLine($"buku fiksi: {Judul} oleh {Penulis}, tahun: {Tahun_terbit}, Status: {(Dipinjam ? "Dipinjam" : "Masih ada")}");
    }

    public string Apa_Judulnya() => Judul;
}


class Non_Fiksi : Buku, Tentang_buku
{

    public Non_Fiksi(string judul, string penulis, int tahun_terbit)
        : base(judul, penulis, tahun_terbit)
    {
    }

    public override void InfoBuku()
    {
        Console.WriteLine($"Buku non Fiksi: {Judul} oleh {Penulis}, Tahun: {Tahun_terbit},, Status: {(Dipinjam ? "Dipinjam" : "masih ada")}");
    }

    public string Apa_Judulnya() => Judul;
}


class Majalah : Buku, Tentang_buku
{

    public Majalah(string judul, string penulis, int tahun_terbit)
        : base(judul, penulis, tahun_terbit)
    {
    }

    public override void InfoBuku()
    {
        Console.WriteLine($"Majalah: {Judul} oleh {Penulis}, Tahun: {Tahun_terbit}, Status: {(Dipinjam ? "Dipinjam" : "Masih ada")}");
    }

    public string Apa_Judulnya() => Judul;
}

class Perpustakaan
{
    private List<Tentang_buku> buku1;
    private List<Tentang_buku> bukudipinjam;
    private const int mentokmeminjam = 3;

    public Perpustakaan()
    {
        buku1 = new List<Tentang_buku>();
        bukudipinjam = new List<Tentang_buku>();
    }

    public void TambahBuku(Tentang_buku buku)
    {
        buku1.Add(buku);
        Console.WriteLine($"buku '{buku.Apa_Judulnya()}' sudah ditambah ke  perpustakaan.");
    }

    public void PerbaruiBuku(string judul, string judulbaru, string penulisbaru, int tahunbaru)
    {
        foreach (var buku in buku1)
        {
            if (buku.Apa_Judulnya() == judul && buku is Buku baseBuku)
            {
                baseBuku.Judul = judulbaru;
                baseBuku.Penulis = penulisbaru;
                baseBuku.Tahun_terbit = tahunbaru;
                Console.WriteLine($"buku '{judul}' Selesai dioerbarui.");
                return;
            }
        }
        Console.WriteLine($"Buku '{judul}' tidak ditemukan.");
    }

    public void TampilkanSemuaBuku()
    {
        if (buku1.Count == 0)
        {
            Console.WriteLine("tidak ada buku di perpustakaan.");
            return;
        }

        Console.WriteLine("\nbuku perpustakaan:");
        foreach (var buku in buku1)
        {
            buku.InfoBuku();
        }
    }

    public void BukuDipinjam(string judul)
    {
        if (bukudipinjam.Count >= mentokmeminjam)
        {
            Console.WriteLine("maaf udah pas 3 buku yang dipinjam.");
            return;
        }

        foreach (var buku in buku1)
        {
            if (buku.Apa_Judulnya() == judul && buku is Buku baseBuku)
            {
                if (!baseBuku.Dipinjam)
                {
                    baseBuku.Dipinjam = true;
                    bukudipinjam.Add(buku);
                    Console.WriteLine($"Buku '{judul}' sukses dipinjam.");
                    return;
                }
                else
                {
                    Console.WriteLine($"Buku '{judul}' sudah dipinjam.");
                    return;
                }
            }
        }
        Console.WriteLine($"Buku '{judul}' tidak ditemukan.");
    }

    public void KembalikanBuku(string judul)
    {
        for (int i = 0; i < bukudipinjam.Count; i++)
        {
            if (bukudipinjam[i].Apa_Judulnya() == judul && bukudipinjam[i] is Buku baseBuku)
            {
                baseBuku.Dipinjam = false;
                bukudipinjam.RemoveAt(i);
                Console.WriteLine($"Buku '{judul}' Berhasil dikembalikan.");
                return;
            }
        }
        Console.WriteLine($"Buku '{judul}' nggak ada di peminjaman kamu.");
    }

    public void TampilkanBukuDipinjam()
    {
        if (bukudipinjam.Count == 0)
        {
            Console.WriteLine("gada buku yang dipinjam.");
            return;
        }

        Console.WriteLine("\nbuku dipinjam:");
        foreach (var buku in bukudipinjam)
        {
            buku.Apa_Judulnya();
        }
    }
}

