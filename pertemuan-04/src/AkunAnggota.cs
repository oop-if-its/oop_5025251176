namespace Pertemuan04;

public class AkunAnggota
{
    public const int MaksPinjaman = 3;

    // get-only: hanya bisa diisi di konstruktor
    public string NomorAnggota { get; }

    // Level 9: init = boleh diisi saat object initializer { Nama = "Budi" }, tidak bisa diubah sesudahnya
    public string Nama { get; init; } = "";

    // Level 9: private set = hanya kode DI DALAM kelas ini yang bisa mengubah
    public int Denda { get; private set; }

    // Level 10: private set juga
    public int JumlahPinjamanAktif { get; private set; }

    public AkunAnggota(string nomorAnggota)
    {
        if (string.IsNullOrWhiteSpace(nomorAnggota))
        {
            throw new ArgumentException("Nomor anggota tidak boleh null, kosong, atau hanya spasi.", nameof(nomorAnggota));
        }
        NomorAnggota = nomorAnggota;
    }

    public void TambahDenda(int rupiah)
    {
        if (rupiah <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rupiah), rupiah, "Denda yang ditambahkan harus lebih dari 0.");
        }
        Denda += rupiah;
    }

    public int BayarDenda(int rupiah)
    {
        if (rupiah <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rupiah), rupiah, "Pembayaran harus lebih dari 0.");
        }
        if (rupiah > Denda)
        {
            throw new InvalidOperationException("Pembayaran melebihi total denda.");
        }
        Denda -= rupiah;
        return Denda; // sisa denda
    }

    // Level 10: internal = hanya bisa dipanggil dari dalam project/pustaka yang sama (mis. Perpustakaan)
    internal void CatatPinjam()
    {
        JumlahPinjamanAktif++;
    }

    internal void CatatKembali()
    {
        if (JumlahPinjamanAktif > 0)
        {
            JumlahPinjamanAktif--;
        }
    }
}































































































































