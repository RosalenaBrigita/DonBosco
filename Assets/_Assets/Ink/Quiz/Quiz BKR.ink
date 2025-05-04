EXTERNAL Quiz(id_answer)
INCLUDE ../Quests/QuestData.ink

#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Sebelum kita bicara soal tugas, aku ingin tahu lebih dulu, Karman...
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
~ set_quiz = true
Kau sudah tahu apa itu BKR? Dan kenapa organisasi ini dibentuk?

* [BKR adalah Barisan Kesatuan Rakyat]
    ~ Quiz("1_1")
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    BKR adalah Barisan Kesatuan Rakyat, Kapten.  
    Organisasi ini dibentuk untuk menjaga keamanan rakyat setelah proklamasi.

    #speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
    Tepat sekali.  
    BKR adalah Barisan Keamanan Rakyat yang dibentuk pada 22 Agustus 1945 oleh sidang PPKI.

    #speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
    Tujuannya jelas: menjaga keamanan rakyat, karena meski Jepang sudah menyerah, mereka masih bersenjata.  
    Dan Belanda… mulai datang lagi lewat NICA.

    -> SejarahBKR

* [BKR adalah Barisan Kemanusiaan Republik]
    ~ Quiz("1_2")
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    BKR adalah Barisan Kemanusiaan Republik... kelompok pemuda yang dibentuk Jepang untuk jaga ketertiban?

    #speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
    Hmm... bukan itu, Bung. Tapi tak apa.

    #speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
    Justru karena itu aku ingin memastikan—yang bergabung dengan BKR harus tahu benar apa yang sedang mereka perjuangkan.  
    Dengarkan baik-baik...

    -> SejarahBKR

    
=== SejarahBKR
~ set_quiz = false
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Setelah proklamasi pada 17 Agustus 1945, negeri kita belum sepenuhnya merdeka.
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Jepang memang menyerah kepada Sekutu, tapi pasukan mereka masih berjaga di gudang-gudang senjata. 
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Sementara itu, Belanda datang kembali melalui NICA—ingin menjajah kita lagi.
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Rakyat butuh perlindungan. Itulah kenapa, dalam sidang PPKI tanggal 22 Agustus, dibentuklah Barisan Keamanan Rakyat.
* [Oh jadi seperti itu sejarahnya]
    -> SejarahBKR2


=== SejarahBKR2 ===
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Awalnya, BKR hanya organisasi keamanan lokal. Tapi semangat rakyat, terutama di Surabaya, membuat kami berkembang lebih jauh.

#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Di kota ini, BKR resmi berdiri pada 9 September. Aku, Moestopo, Sudirman, dan Jonosewojo mulai mengorganisasi para pemuda.

#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Dr. Moestopo kami percayakan sebagai pemimpin. Kami semua punya satu tujuan: mempertahankan kemerdekaan ini. Dengan segala cara yang kami bisa.

* [Jadi, BKR bukan sekadar organisasi biasa?]
    #speaker:Karman #audio:animal_crossing_mid #portrait:karmann
    Ini benar-benar untuk melindungi rakyat... dan melawan penjajah?

    #speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
    Tepat sekali.
    BKR adalah wujud dari semangat rakyat yang tak mau tunduk.  
    Kami ingin semua orang tahu—dari pemuda hingga orang tua—bahwa Indonesia tidak akan menyerah.
    Dan kau, Karman… sekarang adalah bagian dari itu.

-> SejarahBKR3

=== SejarahBKR3 ===
#speaker:Karman #audio:animal_crossing_mid #portrait:karman
Kapten... Apakah rakyat langsung percaya dan bergabung dengan BKR?
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Tidak semuanya, Karman.
Awalnya banyak yang ragu. Ada yang takut pada Jepang, ada pula yang trauma akan perang. Tapi satu hal yang membuat rakyat yakin...
Kami tidak datang dengan senjata saja. Kami datang dengan tekad. Kami datang sebagai saudara.
#speaker:Karman #audio:animal_crossing_mid #portrait:karman
Jadi... bagaimana caranya kalian mengumpulkan para pemuda?
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Kami keliling kampung. Kami bicara dari hati ke hati.  
Tiap malam, kami kumpulkan orang-orang di surau, di pos ronda, bahkan di warung kopi.
Di sana kami bercerita... tentang Proklamasi, tentang tanah yang kita cintai, tentang anak-anak yang harus tumbuh merdeka.
#speaker:Karman #audio:animal_crossing_mid #portrait:karman
Aku jadi ingat... Ayahku juga sering bicara tentang mimpi punya negara sendiri. Tapi beliau meninggal saat pendudukan Jepang...
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Maaf atas kehilanganmu, Karman. Tapi percayalah... sekarang kau berjalan di jejaknya.
* [Apa BKR juga bertempur melawan Jepang?]
    #speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
    Di beberapa tempat, iya.  
    Meski Jepang sudah menyerah, mereka tidak selalu menyerahkan senjatanya dengan mudah.
    #speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
    Bahkan di Surabaya, ada gudang senjata yang dijaga ketat.  
    Kami harus negosiasi, kadang memaksa, kadang berperang.
    -> SejarahBKR4

* [Siapa saja tokoh penting dalam BKR?]
    #speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
    Di Surabaya, ada Dr. Moestopo, pejuang tangguh yang menjadi pemimpin BKR.
    #speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
    Lalu Jonosewojo, yang kelak memimpin BKR Daerah Surabaya. Dan tentu, Sudirman dari Jawa Tengah, yang akhirnya jadi Panglima Besar.

    -> SejarahBKR4

=== SejarahBKR4 ===
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Kami semua datang dari latar belakang berbeda. Ada yang mantan pelajar, ada petani, ada mantan Heiho yang kecewa pada Jepang...
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Tapi kami punya satu tujuan:  
Menjaga kemerdekaan yang sudah diproklamasikan dengan darah dan nyawa.
#speaker:Karman #audio:animal_crossing_mid #portrait:karman
Kapten... Aku ingin jadi bagian dari itu. Bukan hanya mengantarkan pesan—aku ingin benar-benar berjuang.
#speaker:Kapten Soengkono #audio:sungkono #portrait:sungkono
Dan kau akan, Karman. Tapi setiap pejuang harus paham betul apa yang ia perjuangkan.
Teruslah bertanya. Teruslah belajar. Karena sejarah bukan hanya dihafalkan—ia dihidupkan.
Dan sekarang... waktumu untuk melangkah.
-> END

    