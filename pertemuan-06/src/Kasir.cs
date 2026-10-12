namespace Pertemuan06;

public record Peminjaman(Item Item, int HariTerlambat);

public class Kasir
{
    public int TotalDenda(IEnumerable<Peminjaman> daftar)
    {
        ArgumentNullException.ThrowIfNull(daftar);

        int total = 0;
        foreach (var p in daftar)
        {
            total += p.Item.HitungDenda(p.HariTerlambat);
        }
        return total;
    }

    public Item? ItemDenganDendaTertinggi(IEnumerable<Peminjaman> daftar)
    {
        ArgumentNullException.ThrowIfNull(daftar);

        Item? terbaik = null;
        int dendaTertinggi = int.MinValue;
        foreach (var p in daftar)
        {
            int denda = p.Item.HitungDenda(p.HariTerlambat);
            // pakai ">" (bukan ">=") supaya kalau seri, yang pertama muncul tetap menang
            if (denda > dendaTertinggi)
            {
                dendaTertinggi = denda;
                terbaik = p.Item;
            }
        }
        return terbaik;
    }
}
