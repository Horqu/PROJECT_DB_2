using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[SqlUserDefinedAggregate(
    Format.UserDefined, //use clr serialization to serialize the intermediate result
    IsInvariantToNulls = true, //optimizer property
    IsInvariantToDuplicates = false, //optimizer property
    IsInvariantToOrder = false, //optimizer property
    MaxByteSize = -1) //maximum size in bytes of persisted value
]
public class TrendOcen : IBinarySerialize
{
    private int liczbaOcen;
    private decimal sumaOcen;
    private long sumaCzasu;
    private decimal sumaOcenRazyCzas;
    private decimal sumaCzasuKwadrat;

    public void Init()
    {
        this.liczbaOcen = 0;
        this.sumaOcen = 0;
        this.sumaCzasu = 0;
        this.sumaOcenRazyCzas = 0;
        this.sumaCzasuKwadrat = 0;
    }

    public void Accumulate(SqlDecimal ocena, SqlDateTime data)
    {
        if (!ocena.IsNull && !data.IsNull)
        {
            this.liczbaOcen++;
            this.sumaOcen += ocena.Value;
            this.sumaCzasu += data.Value.Ticks;
            this.sumaOcenRazyCzas += ocena.Value * data.Value.Ticks;
            this.sumaCzasuKwadrat += data.Value.Ticks * data.Value.Ticks;
        }
    }

    public void Merge(TrendOcen other)
    {
        this.liczbaOcen += other.liczbaOcen;
        this.sumaOcen += other.sumaOcen;
        this.sumaCzasu += other.sumaCzasu;
        this.sumaOcenRazyCzas += other.sumaOcenRazyCzas;
        this.sumaCzasuKwadrat += other.sumaCzasuKwadrat;
    }

    public SqlDecimal Terminate()
    {
        decimal licznik = this.liczbaOcen * this.sumaOcenRazyCzas - this.sumaOcen * this.sumaCzasu;
        decimal mianownik = this.liczbaOcen * this.sumaCzasuKwadrat - this.sumaCzasu * this.sumaCzasu;

        if (mianownik != 0)
            return new SqlDecimal(licznik / mianownik);
        else
            return SqlDecimal.Null;
    }

    public void Read(System.IO.BinaryReader r)
    {
        this.liczbaOcen = r.ReadInt32();
        this.sumaOcen = r.ReadDecimal();
        this.sumaCzasu = r.ReadInt64();
        this.sumaOcenRazyCzas = r.ReadDecimal();
        this.sumaCzasuKwadrat = r.ReadDecimal();
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(this.liczbaOcen);
        w.Write(this.sumaOcen);
        w.Write(this.sumaCzasu);
        w.Write(this.sumaOcenRazyCzas);
        w.Write(this.sumaCzasuKwadrat);
    }
}
