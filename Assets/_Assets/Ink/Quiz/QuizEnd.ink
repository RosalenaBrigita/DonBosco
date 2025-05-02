EXTERNAL Quiz(id_answer)
INCLUDE ../Quests/QuestData.ink

#speaker:null #audio:null #portrait:null
Perjuanganmu pada penyerangan di gudang Don Bosco mendapat perhatian dari para petinggi BKR
Terdapat banyak pertanyaan yang terpikirkan olehmu
Bagaimana jika sejak awal Indonesia tidak dijajah?
Apa jadinya Indonesia tanpa para pejuang yang telah berjasa besar?
Bagaimana jika Indonesia tidak pernah mendapat kemerdekaan?
Perundingan di Don Bosco bersama atasan Jepang juga memunculkan pertanyaan di kepalamu.
~ set_quiz = true
Mengapa Don Bosco menjadi target utama dalam perebutan gudang senjata? 
* [Karena merupakan gudang senjata terbesar di Asia Tenggara]
    ~ Quiz("3_1")
    Benar, Gudang Don Bosco merupakan pusat persenjataan terbesar di Asia Tenggara pada masa itu.
    Menguasai tempat itu berarti memperkuat kekuatan BKR dan mempertahankan kedaulatan Indonesia yang baru saja diproklamasikan.

    -> quiz4
* [Karena hanya menyimpan senjata-senjata kecil untuk latihan]
    ~ Quiz("3_2")
    Jawabanmu kurang tepat, Gudang Don Bosco bukan sekadar tempat penyimpanan senjata latihan, 
    melainkan markas besar persenjataan penting untuk seluruh wilayah Asia Tenggara.

    -> quiz4

===quiz4===
Kemana hasil perebutan senjata dari gudang Don Bosco dipakai? 
* [Dikirim ke Jakarta oleh Bung Tomo]
    ~ Quiz("7_1")
    Benar, sebagian besar senjata hasil perebutan Don Bosco segera dikirim ke Jakarta, dipimpin oleh Bung Tomo.
    Langkah ini dilakukan untuk memperkuat pertahanan nasional dari ancaman Sekutu dan NICA.

    -> quiz5
* [Dihancurkan untuk menghindari jatuh ke tangan Sekutu]
    ~ Quiz("7_2")
    Itu tidak sepenuhnya benar.
    Senjata dari Gudang Don Bosco justru dijaga dengan ketat dan banyak yang dikirim ke Jakarta.
    Penghancuran hanya dilakukan jika benar-benar tidak ada pilihan untuk mempertahankannya.

    -> quiz5
 
 
 ===quiz5===
Siapa yang menandatangani surat diplomasi dengan Komandan Batalyon Jepang, Mayor Hashimoto? 
* [Moestopo]
    ~ Quiz("8_1")
    Salah, meskipun dr. Moestopo adalah tokoh penting dalam perjuangan, surat diplomasi di Don Bosco ditandatangani oleh tokoh lain 
    yang saat itu bertugas untuk berunding langsung dengan pihak Jepang, yaitu M.Yasin dan Bung Tomo.

    ->END
* [M. Yasin]
    ~ Quiz("8_2")
    Benar. Surat perjanjian diplomasi dengan Komandan Batalyon Jepang, Mayor Hashimoto, ditandatangani oleh M. Yasin dan Bung Tomo.
    Mereka mewakili pejuang Indonesia dalam upaya damai sebelum terjadinya perebutan secara terbuka.
~ set_quiz = false
    ->END