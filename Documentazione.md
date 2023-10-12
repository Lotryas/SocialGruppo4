# Documentazione progetto Social - Gruppo 4

## Modelli

### Entity
Classe base per tutti gli altri modelli.

Proprietà:
- `int Id`

Metodi:
- `string ToString()`

    Sovrascrive il metodo virtual di object per definire
    la visualizzazione in console di tutte le proprietà di
    questo e degli oggetti creati tramite classi figlie.

- `void FromDictionary(Dictionary<string, string> riga)`

    Assegna i valori di un record del database alle rispettive
    proprietà dell'oggetto su cui si chiama questo metodo.

- `string PulisciApici(string s)`

    Metodo statico per sanitizzare dai singoli apici le stringhe
    che devono essere memorizzate in database.

### Utente : Entity
Classe figlia di Entity che rappresenta un utente.

Proprietà:
- `string Nominativo`
- `bool Amministratore`
- `string Email`
- `string Numero`

    Un numero di cellulare italiano.

- `string Residenza`
- `string CodiceFiscale`

    Un codice fiscale italiano.

- `string PasswordHash`

    La password non viene salvata in chiaro in database,
    quindi questa proprietà contiene dei bytes in formato
    SHA2_512.

Metodi:
- `string GetRandomPassword(int length)`

    Genera una password random di lunghezza `length` in
    formato a-zA-z0-9.

