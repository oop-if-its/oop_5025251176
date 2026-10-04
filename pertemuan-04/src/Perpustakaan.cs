namespace Pertemuan04;

public class Perpustakaan
{
    // ---- Level 7: list asli PRIVATE, tidak bisa disentuh dari luar ----
    private readonly List<Buku> _daftarBuku = new();

    // Pihak luar hanya dapat "jendela baca-saja". Tidak ada Add/Clear/Remove.
    public IReadOnlyList<Buku> DaftarBuku => _daftarBuku.AsReadOnly();

    public int JumlahJudul => DaftarBuku.Count;

    public void Tambah(Buku buku)
    {
        if (buku == null)
        {
            throw new ArgumentNullException(nameof(buku));
        }
        if (Cari(buku.Isbn) != null)
        {
            throw new InvalidOperationException($"Buku dengan ISBN {buku.Isbn} sudah ada di koleksi.");
        }
        _daftarBuku.Add(buku);
    }

    public Buku? Cari(string isbn)
    {
        // Pencocokan persis (tanpa normalisasi). Kalau tidak ketemu, Find mengembalikan null.
        return _daftarBuku.Find(b => b.Isbn == isbn);
    }

    // ---- Level 10: Tell, Don't Ask ----
    public void PinjamBuku(string isbn, AkunAnggota akun)
    {
        if (akun == null)
        {
            throw new ArgumentNullException(nameof(akun));
        }

        Buku? buku = Cari(isbn);
        if (buku == null)
        {
            throw new ArgumentException($"Buku dengan ISBN {isbn} tidak ditemukan.", nameof(isbn));
        }
        if (akun.Denda > 0)
        {
            throw new InvalidOperationException("Akun masih punya denda, lunasi dulu sebelum meminjam.");
        }
        if (akun.JumlahPinjamanAktif >= AkunAnggota.MaksPinjaman)
        {
            throw new InvalidOperationException($"Batas peminjaman ({AkunAnggota.MaksPinjaman} buku) sudah tercapai.");
        }

        buku.Pinjam();      // boleh melempar kalau stok habis -> akun belum tercatat apa-apa
        akun.CatatPinjam();
    }

    public void KembalikanBuku(string isbn, AkunAnggota akun)
    {
        if (akun == null)
        {
            throw new ArgumentNullException(nameof(akun));
        }

        Buku? buku = Cari(isbn);
        if (buku == null)
        {
            throw new ArgumentException($"Buku dengan ISBN {isbn} tidak ditemukan.", nameof(isbn));
        }
        if (akun.JumlahPinjamanAktif == 0)
        {
            throw new InvalidOperationException("Akun tidak sedang meminjam buku apa pun.");
        }

        buku.Kembalikan();
        akun.CatatKembali();
    }
}