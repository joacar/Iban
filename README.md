# IBAN

An implementation of the [modulo 97](https://en.wikipedia.org/wiki/International_Bank_Account_Number#Modulo_operation_on_IBAN) algorithm for calculating checksum avoiding use of large integers and allow operation in unsigned integers (< 2^31).
Properties of [congruence relations](https://en.wikipedia.org/wiki/Congruence_relation), more specifically [this](https://math.stackexchange.com/questions/1918217/the-mod97-operation-in-iban-validation), give the correctnes of algorithm

## Example

Lets take iban number for a Croatian bank and disect the steps.

1. Move last four to the end

    `CR23015108410023612345 -> 015108410023612345CR23`

1. Replace characters with digits

    `015108410023612345CR23 -> 015108410023612345122723`

1. Start iteration over nine digit numbers
    1. `N = 0151084410, d = N % 97 = 78`
    1. `N = _78_0260123, d = N % 97 = 77`
    1. `N = _77_451227, d = N % 97 = 25`
    1. `N = _25_23, d = N % 97 = 1`

1. If `d == 1` then valid; otherwise invalid.

The implemention make us of a specialized "iterator" that perform step 1 and 2 without any allocations.