using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[SqlUserDefinedAggregate(
    Format.UserDefined,
    IsInvariantToNulls = true,
    IsInvariantToDuplicates = false,
    IsInvariantToOrder = false,
    MaxByteSize = -1)
]
public class CzestoscPiatki : IBinarySerialize
{
    private int liczbaOcen;
    private int liczbaPiatek;

    public void Init()
    {
        this.liczbaOcen = 0;
        this.liczbaPiatek = 0;
    }

    public void Accumulate(SqlDecimal ocena)
    {
        if (!ocena.IsNull)
        {
            this.liczbaOcen++;
            if (ocena.Value == 5.0m)
            {
                this.liczbaPiatek++;
            }
        }
    }

    public void Merge(CzestoscPiatki other)
    {
        this.liczbaOcen += other.liczbaOcen;
        this.liczbaPiatek += other.liczbaPiatek;
    }

    public SqlString Terminate()
    {
        if (this.liczbaOcen > 0)
        {
            decimal czestosc = (decimal)this.liczbaPiatek / this.liczbaOcen;
            return new SqlString($"Ilość piątek: {this.liczbaPiatek}, Część piątek z wszystkich ocen: {czestosc.ToString("P2")}");
        }
        else
        {
            return new SqlString("Brak ocen.");
        }
    }

    public void Read(System.IO.BinaryReader r)
    {
        this.liczbaOcen = r.ReadInt32();
        this.liczbaPiatek = r.ReadInt32();
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(this.liczbaOcen);
        w.Write(this.liczbaPiatek);
    }
}
