namespace Pertemuan04;

public class Buku
{
    // ---- Level 1: field PRIVATE (diawali _) ----
    // readonly = hanya boleh diisi sekali (di konstruktor), setelah itu terkunci.
    private readonly string _isbn;
    private readonly string _judul;
    private readonly int _stokTotal;
    private int _stokTersedia; // ini berubah-ubah (Pinjam/Kembalikan), jadi tidak readonly

    // ---- Level 8: backing field dengan nilai awal 7 ----
    private int _batasHariPinjam = 7;

    // ---- Level 1: properti read-only (hanya get, tanpa setter) ----
    public string Isbn => _isbn;
    public string Judul => _judul;
    public int StokTotal => _stokTotal;
    public int StokTersedia => _stokTersedia;

    // ---- Level 8: setter dengan validasi ----
    public int BatasHariPinjam
    {
        get => _batasHariPinjam;
        set
        {
            if (value < 1 || value > 30)
            {
                // Throw SEBELUM menyentuh _batasHariPinjam, jadi nilai lama aman.
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, "BatasHariPinjam harus di antara 1 sampai 30 hari.");
            }
            _batasHariPinjam = value;
        }
    }

    public Buku(string isbn, string judul, int stokTotal)
    {
        // ---- Level 2: validasi di AWAL, sebelum ada field yang diisi ----
        if (string.IsNullOrWhiteSpace(judul))
        {
            throw new ArgumentException("Judul tidak boleh null, kosong, atau hanya spasi.", nameof(judul));
        }
        if (stokTotal < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(stokTotal), stokTotal, "Stok total tidak boleh negatif.");
        }

        // ---- Level 6: validasi & normalisasi ISBN (lempar ArgumentException kalau tidak sah) ----
        string isbnBersih = NormalisasiIsbn(isbn);

        // ---- Level 1: baru isi field setelah SEMUA validasi lolos ----
        _isbn = isbnBersih;
        _judul = judul;
        _stokTotal = stokTotal;
        _stokTersedia = stokTotal;
    }

    // ---- Level 6: helper private (pemakai dari luar tidak perlu tahu caranya) ----
    private static string NormalisasiIsbn(string isbn)
    {
        if (isbn == null)
        {
            throw new ArgumentException("ISBN tidak boleh null.", nameof(isbn));
        }

        // Buang semua '-' dan spasi
        string bersih = isbn.Replace("-", "").Replace(" ", "");

        if (bersih.Length != 13)
        {
            throw new ArgumentException("ISBN harus tepat 13 digit.", nameof(isbn));
        }

        int total = 0;
        for (int i = 0; i < 13; i++)
        {
            char c = bersih[i];
            if (c < '0' || c > '9')
            {
                throw new ArgumentException("ISBN hanya boleh berisi angka (selain '-' dan spasi).", nameof(isbn));
            }

            int digit = c - '0';
            // posisi ke-1 (indeks 0) bobot 1, ke-2 (indeks 1) bobot 3, dst.
            total += (i % 2 == 0) ? digit : digit * 3;
        }

        if (total % 10 != 0)
        {
            throw new ArgumentException("Digit cek ISBN-13 tidak valid.", nameof(isbn));
        }

        return bersih;
    }

    // ---- Level 3 ----
    public void Pinjam()
    {
        if (_stokTersedia == 0)
        {
            throw new InvalidOperationException("Stok habis, buku tidak bisa dipinjam.");
        }
        _stokTersedia--;
    }

    // ---- Level 4 ----
    public void Kembalikan()
    {
        if (_stokTersedia == _stokTotal)
        {
            throw new InvalidOperationException("Semua eksemplar sudah ada di perpustakaan, tidak ada yang bisa dikembalikan.");
        }
        _stokTersedia++;
    }

    // ---- Level 5: properti TERHITUNG (tanpa field, tanpa setter) ----
    public double PersentaseTersedia
    {
        get
        {
            if (_stokTotal == 0)
            {
                return 0; // hindari pembagian dengan nol (hasilnya NaN)
            }
            // (double) dulu supaya pembagiannya desimal, bukan pembagian bilangan bulat
            return (double)_stokTersedia / _stokTotal * 100;
        }
    }

    public string Status
    {
        get
        {
            return _stokTersedia > 0 ? "Tersedia" : "Habis";
        }
    }
}