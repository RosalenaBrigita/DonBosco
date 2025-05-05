EXTERNAL Quiz(id_answer)
EXTERNAL getCurrentMoral()
INCLUDE ../Quests/QuestData.ink

#speaker:Bung Tomo #portrait:soetomo #audio:soetomo  
Kita hanya punya satu kesempatan. Karman, bagaimana menurutmu cara terbaik menyapa Komandan Jepang agar tetap menjaga wibawa, tapi tak langsung memicu konflik?

* [Langsung menuntut dengan nada tinggi, menunjukkan ketegasan]  
    ~ Quiz("4_1")  
    -> setelahQuiz4_1  

* [Memberi salam militer dan memperkenalkan diri dengan tegas namun hormat]  
    ~ Quiz("4_2")  
    -> setelahQuiz4_2  


=== setelahQuiz4_1 ===
#speaker:Karman #portrait:karman #audio:animal_crossing_mid  
Kita langsung tunjukkan niat kita. Jangan beri mereka kesempatan bermain-main!
#speaker:null #portrait:null #audio:null
(Karman melangkah cepat ke tengah ruangan tanpa basa-basi, lalu bicara keras) 
#speaker:Karman #portrait:karman #audio:animal_crossing_mid  
Komandan! Kami datang untuk menuntut penyerahan semua persenjataan dan logistik. Jangan coba-coba berdalih!  
#speaker:Komandan Jepang #portrait:jepang #audio:jepang  
Nada keras itu tidak pantas dalam perundingan. Jika kalian tidak datang untuk berbicara dengan kepala dingin, maka lebih baik kalian pergi.  
#speaker:null #portrait:null #audio:null
(Bung Tomo menatap Karman sejenak, wajahnya menegang)
#speaker:Bung Tomo #portrait:soetomo #audio:soetomo
Karman... kita di sini bukan untuk cari ribut.  
-> Diplomacy2

=== setelahQuiz4_2 ===
#speaker:Karman #portrait:karman #audio:animal_crossing_mid
Lebih baik kita tetap jaga protokol. Tunjukkan hormat, tapi tetap tegas.
#speaker:null #portrait:null #audio:null
(Karman berdiri tegap dan memberi salam singkat dengan nada sopan namun jelas) 
#speaker:Karman #portrait:karman #audio:animal_crossing_mid
Saya Karman, bersama Bung Tomo dan M. Yasin. Kami datang sebagai wakil pemuda Surabaya, untuk mencari penyelesaian damai.    
#speaker:Komandan Jepang #portrait:jepang #audio:jepang  
Sikap kalian mencerminkan kehormatan. Silakan duduk. Saya akan dengarkan maksud kedatangan ini. 
#speaker:null #portrait:null #audio:null
(M. Yasin mengangguk pelan pada Karman)
#speaker:M.Yasin #portrait:m_yasin #audio:m_yasin
Pendekatan yang tepat. Kita butuh kepala dingin agar berhasil. 
-> Diplomacy2

=== Diplomacy2 ===
#speaker:Komandan Jepang #portrait:jepang #audio:jepang
Apa ada yg ingin klian bicarakan?

* [Kami meminta senjata dan perbekalan diserahkan secara damai]  
    ~ Quiz("5_1")
    -> setelahQuiz5_1

* [Kalau tidak menyerahkan senjata sekarang, kami akan ambil paksa]  
    ~ Quiz("5_2")
    -> setelahQuiz5_2

=== setelahQuiz5_1 ===
#speaker:M.Yasin #portrait:m_yasin #audio:m_yasin
Kami mewakili rakyat Surabaya yang sedang berjuang mempertahankan kemerdekaan. 
Kami meminta senjata dan perbekalan diserahkan secara damai, tanpa perlawanan.
#speaker:Komandan Jepang #portrait:jepang #audio:jepang
Tuntutan yang langsung dan jelas. Sikap kalian membuat saya berpikir untuk mendengarkan lebih jauh.
#speaker:null #portrait:null #audio:null
(Komandan Jepang menyilangkan tangan di depan dada, memperhatikan kalian dengan serius)
-> Diplomacy3

=== setelahQuiz5_2 ===
#speaker:Bung Tomo #portrait:soetomo #audio:soetomo
Jika kalian tidak menyerahkan senjata sekarang juga, rakyat Surabaya akan bertindak sendiri!
#speaker:Komandan Jepang #portrait:jepang #audio:jepang
Nada seperti itu hanya akan membawa kita ke jalan buntu.
#speaker:null #portrait:null #audio:null
(Komandan Jepang terlihat tegang, tangannya perlahan menyentuh gagang pedangnya)
-> Diplomacy3

=== Diplomacy3 ===
#speaker:Komandan Jepang #portrait:jepang #audio:jepang
Permintaan kalian sangat besar. Namun, kami masih memiliki perintah dari atasan.

* [Kami tidak akan ganggu pasukan Jepang jika senjata diserahkan]  
    ~ Quiz("6_1")
    -> setelahQuiz6_1

* [Kami tidak peduli. Serahkan senjatanya sekarang juga!]  
    ~ Quiz("6_2")
    -> setelahQuiz6_2

=== setelahQuiz6_1 ===
#speaker:Karman #portrait:karman #audio:animal_crossing_mid
Kami tidak ingin konflik. Senjata itu untuk mempertahankan rakyat kami dari ancaman luar.
Kami tidak akan mengganggu pasukan Jepang yang masih bertugas di sini.
#speaker:Komandan Jepang #portrait:jepang #audio:jepang
Hm... kalian tahu cara berbicara seperti diplomat sejati. Itu sikap yang terhormat.
#speaker:null #portrait:null #audio:null
(Komandan Jepang menghela napas panjang, mulai melunak)
-> PilihanDiplomasiAkhir

=== setelahQuiz6_2 ===
#speaker:Bung Tomo #portrait:soetomo #audio:soetomo
Kalian tak bisa sembunyi di balik perintah. 
Jika kalian menolak, rakyat Surabaya akan mengambil apa yang mereka butuhkan dengan paksa jika perlu.
#speaker:Komandan Jepang #portrait:jepang #audio:jepang
Apakah kalian datang membawa ancaman? Ini bukan cara untuk berbicara!
#speaker:null #portrait:null #audio:null
(Suasana ruangan berubah mencekam. Beberapa perwira Jepang di belakang komandan mulai gelisah)
-> PilihanDiplomasiAkhir

=== PilihanDiplomasiAkhir ===
#speaker:Komandan Jepang #portrait:jepang #audio:jepang
Aku tidak bisa menyerahkan semuanya. Namun, aku bisa memberikan sebagian dari senjata dan perbekalan yang ada di gudang ini. 
Itu adalah tawaran terbaik saya.

* [Kami tetap meminta semuanya]
    -> PilihTetapMeminta

* [Baiklah, kami terima sebagian saja demi menghindari konflik]
    -> PilihSetujuSebagian

=== PilihTetapMeminta ===
~ temp moral = getCurrentMoral()

{ moral >= 50:
    #speaker:null #portrait:null #audio:null
    Setelah berpikir panjang, Komandan Jepang akhirnya mengangguk dengan berat hati.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Baiklah. Semua akan menjadi milik kalian.
    -> EndingSuksesTotal
- else:
    -> CekBenderaSaatGagal
}

=== CekBenderaSaatGagal ===
{ set_bendera:
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Kalian datang dengan bendera. Itu menunjukkan kalian masih punya niat baik. Aku beri kalian satu kesempatan: ambil tawaran sebagian, atau pergi tanpa apa pun.
    -> TanyaLagi
- else:
    #speaker:null #portrait:null #audio:null
    (Komandan Jepang berdiri dengan amarah)
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Tidak ada lagi yang perlu dibicarakan! Keluar dari ruangan ini sekarang!
    -> EndingGagal
}

=== TanyaLagi ===
    * [Setuju sebagian saja]
        -> EndingTerpaksa
    * [Tetap meminta semua]
        #speaker:Komandan Jepang #portrait:jepang #audio:jepang
        Maka tidak ada lagi yang bisa dibicarakan.
        -> EndingGagal
        
=== PilihSetujuSebagian ===
~ temp moral = getCurrentMoral()

{ moral >= 50:
    #speaker:null #portrait:null #audio:null
    Komandan Jepang mengangguk dengan lega.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Kalian adalah negosiator yang bijaksana. Barang-barang akan diserahkan segera.
    -> EndingSuksesSebagian
- else:
    #speaker:null #portrait:null #audio:null
    Komandan Jepang tetap tampak tidak puas.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Kalian beruntung saya tidak ingin menciptakan kekacauan. Ambil apa yang bisa kalian dapatkan, lalu pergi.
    -> EndingTerpaksa
}

=== EndingSuksesTotal ===
#speaker:null #portrait:null #audio:null
Kalian berhasil mendapatkan seluruh senjata dan perbekalan tanpa perlawanan. Ini adalah kemenangan diplomasi yang luar biasa!
~ sukses_total = true
{ set_bendera:
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Karena kalian membawa bendera itu... aku tahu bahwa Republik ini benar-benar hidup. Sebagai tambahan, Aku akan berkata jujur... 
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Tak lama lagi, kapal-kapal Belanda dan pasukan Sekutu akan tiba melalui pelabuhan. Mereka bilang ingin mengambil tawanan perang... tapi kita tahu itu hanya alasan.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Radio menangkap sinyal bahwa kapal Sekutu akan berlabuh di pelabuhan Tanjung Perak. Mereka membawa logistik dan satuan bersenjata lengkap.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Mereka ingin menginjakkan kaki lagi di tanah ini—seolah kemerdekaan kalian tak berarti. Bersiaplah. Kemenangan hari ini bukan akhir... tapi awal dari perjuangan yang lebih besar.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Jangan tertipu dengan bendera Inggris. Di balik mereka ada NICA—Belanda yang ingin kembali berkuasa. Kalian harus waspada terhadap tipu daya mereka.
}
-> END

=== EndingSuksesSebagian ===
#speaker:null #portrait:null #audio:null
Sebagian perbekalan dan senjata kini berada di tangan kalian. Meskipun bukan kemenangan total, ini tetap pencapaian yang besar.
~ sukses_sebagian = true
 
{ set_bendera:
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Karena kalian membawa bendera itu... aku tahu bahwa Republik ini benar-benar hidup. Sebagai tambahan, Aku akan berkata jujur... 
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Tak lama lagi, kapal-kapal Belanda dan pasukan Sekutu akan tiba melalui pelabuhan. Mereka bilang ingin mengambil tawanan perang... tapi kita tahu itu hanya alasan.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Radio menangkap sinyal bahwa kapal Sekutu akan berlabuh di pelabuhan Tanjung Perak. Mereka membawa logistik dan satuan bersenjata lengkap.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Mereka ingin menginjakkan kaki lagi di tanah ini—seolah kemerdekaan kalian tak berarti. Bersiaplah. Kemenangan hari ini bukan akhir... tapi awal dari perjuangan yang lebih besar.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Jangan tertipu dengan bendera Inggris. Di balik mereka ada NICA—Belanda yang ingin kembali berkuasa. Kalian harus waspada terhadap tipu daya mereka.
}
-> END

=== EndingTerpaksa ===
#speaker:null #portrait:null #audio:null
Negosiasi berjalan sulit, tetapi setidaknya kalian mendapatkan sesuatu daripada tidak sama sekali. Ini adalah kompromi yang pahit.
~ ending_terpaksa = true
{ set_bendera:
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Karena kalian membawa bendera itu... aku tahu bahwa Republik ini benar-benar hidup. Sebagai tambahan, Aku akan berkata jujur... 
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Tak lama lagi, kapal-kapal Belanda dan pasukan Sekutu akan tiba melalui pelabuhan. Mereka bilang ingin mengambil tawanan perang... tapi kita tahu itu hanya alasan.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Radio menangkap sinyal bahwa kapal Sekutu akan berlabuh di pelabuhan Tanjung Perak. Mereka membawa logistik dan satuan bersenjata lengkap.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Mereka ingin menginjakkan kaki lagi di tanah ini—seolah kemerdekaan kalian tak berarti. Bersiaplah. Kemenangan hari ini bukan akhir... tapi awal dari perjuangan yang lebih besar.
    #speaker:Komandan Jepang #portrait:jepang #audio:jepang
    Jangan tertipu dengan bendera Inggris. Di balik mereka ada NICA—Belanda yang ingin kembali berkuasa. Kalian harus waspada terhadap tipu daya mereka.
}
-> END

=== EndingGagal ===
#speaker:null #portrait:null #audio:null
Negosiasi gagal total. Tidak ada senjata atau perbekalan yang didapat. Kalian harus mencari cara lain untuk menghadapi situasi ini.
~ ending_gagal = true
-> END