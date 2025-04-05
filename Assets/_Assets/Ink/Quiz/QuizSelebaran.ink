EXTERNAL Quiz(id_answer)
INCLUDE ../Quests/QuestData.ink

#speaker:Moestopo #audio:null #portrait:null
Aku belum mendapat informasi lebih lanjut tentang selebaran yang dijatuhkan pesawat Sekutu. Apa kau tahu tentang itu?
* [Itu menyerukan agar rakyat menyerah ]
    ~ Quiz("2_1")
    #speaker:Karman #audio:null #portrait:null
    Selebaran itu menyerukan agar rakyat menyerah kepada kekuatan Sekutu
    #speaker:Moestopo #audio:null #portrait:null
    Karman, pilihanmu tidak tepat. Kita tidak punya waktu untuk keraguan. Menyerah tidak akan pernah menjadi bagian dari perlawanan ini. 
    Ingat, kita berjuang untuk kemerdekaan, bukan untuk menyerah pada ancaman. Kembali fokus pada perjuangan kita.
    -> Lanjut
    
* [Itu berisi ancaman kedatangan sekutu]
    ~ Quiz("2_2")
    #speaker:Karman #audio:null #portrait:null
    Selebaran itu berisi ancaman tentang kedatangan Sekutu dan perintah untuk bersiap-siap
    #speaker:Moestopo #audio:null #portrait:null
    Ah, jadi itu yang terjadi. Terima kasih telah memberitahuku. Saya belum mendapat laporan tentang selebaran itu. Ancaman yang tertulis di sana tidak akan membuat kami mundur. 
    Justru, itu semakin menegaskan bahwa perlawanan kita harus dimulai lebih cepat. Kita harus segera merebut Gudang Don Bosco, kalau tidak kita akan tertinggal.
    -> Lanjut

===Lanjut===
#speaker:Moestopo #audio:null #portrait:null
Karman, sekarang kita harus fokus pada yang lebih besar. Gudang Don Bosco adalah salah satu gudang senjata terbesar yang dikuasai oleh Jepang di Asia Tenggara. 
Di sanalah mereka menyimpan persenjataan paling canggih yang mereka miliki, dan itu adalah kunci utama untuk perlawanan kita.
* [Kenapa Gudang Don Bosco]
    #speaker:Karman #audio:null #portrait:null
    Kenapa Gudang Don Bosco begitu penting, Bung? Bukankah kita sudah mempersiapkan diri dengan senjata yang ada?
-> Lanjut2

===Lanjut2====
    #speaker:Moestopo #audio:null #portrait:null
    Kita sudah berjuang dengan apa yang kita punya, Karman. Tetapi, gudang itu berisi persenjataan yang jauh lebih besar, 
    lebih kuat—senjata yang bisa membuat kita jauh lebih siap dalam pertempuran melawan Sekutu yang sudah semakin dekat. Itu bukan hanya soal senjata, tetapi juga tentang moral. 
    Jika kita berhasil merebut Gudang Don Bosco, itu akan mengubah arah perjuangan kita.
    #speaker:Karman #audio:null #portrait:null
    Saya mengerti, Pak. Ini bisa memberi kita keuntungan besar.
    #speaker:Moestopo #audio:null #portrait:null
    Benar. Jika kita merebutnya, kita akan mendapatkan lebih dari sekadar senjata. Kita akan mendapatkan simbol kekuatan.
    Ini bukan hanya soal perang, tapi soal menunjukkan kepada rakyat Surabaya bahwa kita mampu melawan. Ini adalah kesempatan yang tidak boleh terlewatkan.
    Sebelum kita bergerak, ada satu hal lagi yang perlu kau pahami, Karman. Sebelum menyerang Gudang Don Bosco, kita harus belajar dari penyerangan yang sudah berhasil. 
    Di dua lokasi penting—Gudang Sambongan dan Kaliasin—pemuda kita berhasil merebut senjata dari Jepang. 
    Di sana, kita belajar banyak taktik yang bisa kita gunakan untuk operasi besar ini.
    #speaker:Karman #audio:null #portrait:null
    Jadi, kita perlu mempelajari bagaimana mereka melakukannya, Bung?
    #speaker:Moestopo #audio:null #portrait:null
    Betul. Aku ingin kau berbicara dengan warga yang menyaksikan penyerangan dan dengan Polisi Istimewa yang terlibat. 
    Mereka akan memberikan wawasan berharga tentang bagaimana mereka mengatasi situasi tersebut. Ini akan membantu kita merencanakan serangan ke Gudang Don Bosco dengan lebih matang.
* [ke Kaliasin]
    -> KaliasinBranch
* [ke Sambongan]
    -> SambonganBranch

=== KaliasinBranch ===
#speaker:Karman #audio:null #portrait:null
Baik aku akan cari informasi di Kaliasin
~ start_quest = "InformasiKaliasin"
-> END

=== SambonganBranch ===
#speaker:Karman #audio:null #portrait:null
Baik aku akan cari informasi di Sambongan
~ start_quest = "InformasiSambongan"
-> END


    