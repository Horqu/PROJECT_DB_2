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
public class RozkladOcen : IBinarySerialize
{
    private int liczbaOcen;
    private int oceny2;
    private int oceny3;
    private int oceny35;
    private int oceny4;
    private int oceny45;
    private int oceny5;

    public void Init()
    {
        this.liczbaOcen = 0;
        this.oceny2 = 0;
        this.oceny3 = 0;
        this.oceny35 = 0;
        this.oceny4 = 0;
        this.oceny45 = 0;
        this.oceny5 = 0;
    }

    public void Accumulate(SqlDecimal ocena)
    {
        if (!ocena.IsNull)
        {
            this.liczbaOcen++;
            switch (ocena.Value)
            {
                case 2.0m:
                    this.oceny2++;
                    break;
                case 3.0m:
                    this.oceny3++;
                    break;
                case 3.5m:
                    this.oceny35++;
                    break;
                case 4.0m:
                    this.oceny4++;
                    break;
                case 4.5m:
                    this.oceny45++;
                    break;
                case 5.0m:
                    this.oceny5++;
                    break;
            }
        }
    }

    public void Merge(RozkladOcen other)
    {
        this.liczbaOcen += other.liczbaOcen;
        this.oceny2 += other.oceny2;
        this.oceny3 += other.oceny3;
        this.oceny35 += other.oceny35;
        this.oceny4 += other.oceny4;
        this.oceny45 += other.oceny45;
        this.oceny5 += other.oceny5;
    }

    public SqlString Terminate()
    {
        if (this.liczbaOcen == 0)
            return "Brak ocen.";
        else
            return $"2.0: {((double)this.oceny2 / this.liczbaOcen) * 100}% | 3.0: {((double)this.oceny3 / this.liczbaOcen) * 100}% | 3.5: {((double)this.oceny35 / this.liczbaOcen) * 100}% | 4.0: {((double)this.oceny4 / this.liczbaOcen) * 100}% | 4.5: {((double)this.oceny45 / this.liczbaOcen) * 100}% | 5.0: {((double)this.oceny5 / this.liczbaOcen) * 100}%";
    }

    public void Read(System.IO.BinaryReader r)
    {
        this.liczbaOcen = r.ReadInt32();
        this.oceny2 = r.ReadInt32();
        this.oceny3 = r.ReadInt32();
        this.oceny35 = r.ReadInt32();
        this.oceny4 = r.ReadInt32();
        this.oceny45 = r.ReadInt32();
        this.oceny5 = r.ReadInt32();
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(this.liczbaOcen);
        w.Write(this.oceny2);
        w.Write(this.oceny3);
        w.Write(this.oceny35);
        w.Write(this.oceny4);
        w.Write(this.oceny45);
        w.Write(this.oceny5);
    }
}
