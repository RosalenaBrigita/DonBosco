#speaker:Kapten Soengkono #audio:null #portrait:sungkono
Apa yang kau butuhkan, Bung?
-> END

===CompletePesawat===
#speaker:Kapten Soengkono #audio:null #portrait:sungkono
Tolong segera antarkan kabar ini ke Moestopo di kantor BKR, Karman.
#speaker:Karman #audio:null #portrait:karman
Baik kapten, aku akan segera kesana.
->END

===CompleteGabung===
#speaker:Kapten Soengkono #audio:null #portrait:sungkono
Pergilah ke Lapangan Tambaksari, Bung Tomo sedang melakukan Rapat Akbar di sana. Kau pasti mendapatkan sesuatu sepulang dari sana.
#speaker:Karman #audio:null #portrait:karman
Baik kapten, aku akan segera kesana.
->END

=== GabungBKR ===
#speaker:Kapten Soengkono #audio:null #portrait:sungkono
Karman! Akhirnya kau datang. Aku sudah dengar banyak tentangmu.
#speaker:Kapten Soengkono
Katanya kau pemuda yang berani, keras kepala… dan tak pernah lari dari masalah.
#speaker:Karman #audio:null #portrait:karman
Heh... Saya cuma melakukan apa yang menurut saya benar, Kapten.
#speaker:Kapten Soengkono #audio:null #portrait:sungkono
Dan itu yang kami butuhkan sekarang.
#speaker:Kapten Soengkono #audio:null #portrait:sungkono
Negeri ini butuh lebih dari sekadar semangat. Ia butuh tindakan.  
Jadi, Karman... apakah kau siap?
* [Saya siap. Katakan saja apa yang harus saya lakukan.]
    -> SiapGabung
* [Kalau saya tak datang... lalu siapa lagi?]
    -> SiapGabung
* [Belum tahu apakah saya pantas... tapi saya akan coba.]
    -> SiapGabung

=== SiapGabung ===
#speaker:Kapten Soengkono #audio:null #portrait:sungkono
Jawaban yang bagus.  
Kau memang belum sepenuhnya siap... tapi siapa di antara kita yang benar-benar siap?
#speaker:Kapten Soengkono #audio:null #portrait:sungkono
Yang penting, kau di sini. Dan kau bersedia. Itu sudah cukup untuk mulai.
-> END

===AjakBicara===
#speaker:Kapten Soengkono #audio:null #portrait:sungkono
Karman, keadaan semakin mendesak. Kau lihat sendiri, pesawat-pesawat itu bukan sekadar pamer kekuatan—mereka adalah tanda. Sekutu akan segera datang ke Surabaya.

#speaker:Kapten Soengkono #audio:null #portrait:sungkono
Kita tak bisa hanya diam. Aku butuh kau untuk menyampaikan pesan ini kepada Moestopo di Markas BKR. Dia harus segera tahu.

* [Siap, Kapten. Tapi... siapa itu Moestopo sebenarnya?]
    #speaker:Kapten Soengkono #audio:null #portrait:sungkono
    Pertanyaan yang bagus. Dr. Moestopo adalah pemimpin BKR Surabaya. 
    Orang yang cerdas dan berani. Dulu dia seorang dokter gigi, tapi sekarang... 
    dialah benteng pertahanan rakyat di kota ini.
    #speaker:Karman #audio:null #portrait:karman
    Seorang dokter jadi pemimpin pasukan?
    #speaker:Kapten Soengkono #audio:null #portrait:sungkono
    Di masa perang, keberanian lebih penting daripada gelar. Dan Moestopo punya keduanya.
    -> Lanjut

* [Aku belum pernah bertemu langsung dengan Moestopo]
    #speaker:Kapten Soengkono #audio:null #portrait:sungkono
    Tak apa, banyak yang belum. Tapi kau akan tahu saat melihatnya. Wibawanya... tak bisa disangkal.
    Dia bukan hanya memimpin pasukan, tapi menyatukan semangat rakyat.
    #speaker:Karman #audio:null #portrait:karman
    Baik, aku akan cari tahu sendiri. 
    -> Lanjut

===Lanjut===
* [Aku siap mengantarkan pesannya]
    #speaker:Karman #audio:null #portrait:karman
    Saya paham, Kapten. Ini bukan hanya pesan biasa, ini soal perjuangan. Saya akan pastikan Moestopo menerimanya, secepat mungkin.
    
    #speaker:Kapten Soengkono #audio:null #portrait:sungkono
    Bagus, Karman. Kau pemuda yang bisa diandalkan. Ambillah selebaran ini dan sampaikan pada Moestopo. 
    Dan ingat, satu pesan ini... bisa mengubah langkah kita selanjutnya.

-> END
