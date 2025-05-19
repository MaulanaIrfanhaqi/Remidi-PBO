public class Book
{
    public int ID { get; set; }
    public string Judul { get; set; }
    public string Penulis { get; set; }
    public int TahunTerbit { get; set; }
    public string Status { get; set; } = "Tersedia";

    public Book() { }

    public Book(int id, string judul, string penulis, int tahun)
    {
        ID = id;
        Judul = judul;
        Penulis = penulis;
        TahunTerbit = tahun;
    }

    public virtual void TampilkanInfo()
    {
        Console.WriteLine($"ID     : {ID}");
        Console.WriteLine($"Judul  : {Judul}");
        Console.WriteLine($"Penulis: {Penulis}");
        Console.WriteLine($"Tahun  : {TahunTerbit}");
        Console.WriteLine($"Status : {Status}");
        Console.WriteLine("-----------------------------------");
    }

}


public class Ebook : Book
{
    public string FormatFile { get; set; }

    public Ebook(int id, string judul, string penulis, int tahun, string format)
        : base(id, judul, penulis, tahun)
    {
        FormatFile = format;
    }

    public override void TampilkanInfo()
    {
        base.TampilkanInfo();
        Console.WriteLine($"Format : {FormatFile}");
        Console.WriteLine("-----------------------------------");
    }

}

public class Perpustakaan
{
    public string Nama { get; set; }
    public string Alamat { get; set; }
    private List<Book> koleksiBuku;

    public Perpustakaan(string nama, string alamat)
    {
        Nama = nama;
        Alamat = alamat;
        koleksiBuku = new List<Book>();
    }

    public void TambahBuku(Book buku)
    {
        koleksiBuku.Add(buku);
    }

    public void TampilkanSemuaBuku()
    {
       
        foreach (var buku in koleksiBuku)
            buku.TampilkanInfo();
    }

    public Book CariBuku(int id)
    {
        return koleksiBuku.FirstOrDefault(b => b.ID == id);
    }

    public void UbahBuku(int id)
    {
        var buku = CariBuku(id);
        if (buku != null)
        {
            Console.Write("Judul baru: ");
            buku.Judul = Console.ReadLine();
            Console.Write("Penulis baru: ");
            buku.Penulis = Console.ReadLine();
            Console.Write("Tahun Terbit baru: ");
            buku.TahunTerbit = int.Parse(Console.ReadLine());
            Console.Write("Status baru (Tersedia/Dipinjam): ");
            buku.Status = Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Buku tidak ditemukan.");
        }
    }

    public void HapusBuku(int id)
    {
        var buku = CariBuku(id);
        if (buku != null)
        {
            koleksiBuku.Remove(buku);
            Console.WriteLine("Buku berhasil dihapus.");
        }
        else
        {
            Console.WriteLine("Buku tidak ditemukan.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Perpustakaan perpustakaan = new Perpustakaan("Perpustakaan Unej", "Jember");

        while (true)
        {
            Console.WriteLine("\n--- Menu Perpustakaan ---");
            Console.WriteLine("1. Tambah Buku");
            Console.WriteLine("2. Tampilkan Semua Buku");
            Console.WriteLine("3. Cari Buku");
            Console.WriteLine("4. Ubah Buku");
            Console.WriteLine("5. Hapus Buku");
            Console.WriteLine("6. Ubah Status Buku");
            Console.WriteLine("7. kembali");
            Console.Write("Pilih menu: ");
            
            string pilihan = Console.ReadLine();

            switch (pilihan)
            {
                case "1":
                    Console.Write("ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Judul: ");
                    string judul = Console.ReadLine();
                    Console.Write("Penulis: ");
                    string penulis = Console.ReadLine();
                    Console.Write("Tahun Terbit: ");
                    int tahun = int.Parse(Console.ReadLine());
                    Console.Write("Jenis (buku/ebook): ");
                    string jenis = Console.ReadLine();
                    if (jenis.ToLower() == "ebook")
                    {
                        Console.Write("Format File: ");
                        string format = Console.ReadLine();
                        perpustakaan.TambahBuku(new Ebook(id, judul, penulis, tahun, format));
                    }
                    else
                    {
                        perpustakaan.TambahBuku(new Book(id, judul, penulis, tahun));
                    }
                    break;

                case "2":
                    perpustakaan.TampilkanSemuaBuku();
                    break;

                case "3":
                    Console.Write("Masukkan ID Buku: ");
                    int cariId = int.Parse(Console.ReadLine());
                    var buku = perpustakaan.CariBuku(cariId);
                    if (buku != null) buku.TampilkanInfo();
                    else Console.WriteLine("Buku tidak ditemukan.");
                    break;

                case "4":
                    Console.Write("Masukkan ID Buku yang ingin diubah: ");
                    perpustakaan.UbahBuku(int.Parse(Console.ReadLine()));
                    break;

                case "5":
                    Console.Write("Masukkan ID Buku yang ingin dihapus: ");
                    perpustakaan.HapusBuku(int.Parse(Console.ReadLine()));
                    break;

                case "6":
                    Console.Write("Masukkan ID Buku: ");
                    int idStatus = int.Parse(Console.ReadLine());
                    var bukuStatus = perpustakaan.CariBuku(idStatus);
                    if (bukuStatus != null)
                    {
                        Console.Write("Ubah status ke (Tersedia/Dipinjam): ");
                        string statusBaru = Console.ReadLine();
                        if (statusBaru.ToLower() == "tersedia" || statusBaru.ToLower() == "dipinjam")
                        {
                            bukuStatus.Status = statusBaru;
                            Console.WriteLine("Status berhasil diubah.");
                        }
                        else
                        {
                            Console.WriteLine("Status tidak valid. Gunakan 'Tersedia' atau 'Dipinjam'.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Buku tidak ditemukan.");
                    }
                    break;


                case "7":
                    return;


                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
        }
    }
}
