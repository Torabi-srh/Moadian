# سامانه مودیان

کد C#

نظام پایانه‌های فروشگاهی و سامانه مودیان


## Installation

```
Add refrence 
```

## Usage

```C#
using Moadian.Dto;
using Moadian.Services;
```

```C# 
var moadian = new Moadian.Moadian("publicKeyFile", "privateKeyFile", "********-****-****-****-************", "**************");
var token = await moadian.GetToken();
var invoices = new List<InvoiceDto>();
var random = new Random();
int randomSerialDecimal = random.Next(999999999);
var now = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
var taxId = moadian.GenerateTaxId(DateTime.Now, randomSerialDecimal);
var header = new InvoiceHeaderDto
{
    inty = item.inty,
    inp = item.inp,
    inno = randomSerialDecimal.ToString(),
    ins = item.ins,
    tins = item.tins,
    tprdis = item.tprdis,
    tdis = item.tdis,
    tvam = item.tvam,
    todam = item.todam,
    tbill = item.tbill,
    setm = item.setm,
    cap = item.cap,
    insp = item.insp,
    tvop = item.tvop,
    tax17 = item.Tax17,
    indatim = item.indatim,
    indati2m = item.Indati2m,
    taxid = taxId,
    bid = "{BID}",
};
var body = new InvoiceBodyDto
{
    sstid = item.sstid,
    sstt = item.sstt,
    mu = item.mu,
    am = item.am,
    fee = item.fee,
    prdis = item.prdis,
    dis = item.dis,
    adis = item.adis,
    vra = item.vra,
    vam = item.vam,
    tsstam = item.tsstam,
};
var payment = new InvoicePaymentDto
{
    Iinn = item.iinn,
    Acn = item.acn,
    Trmn = item.trmn,
    Trn = item.trn,
};
invoices.Add(new()
{
    body = new() { body },
    header = header,
    payments = new() { payment }
});
var invoice = await moadian.SendInvoice(new Packet(Moadian.Constants.PacketType.INVOICE_V01, invoices));
```