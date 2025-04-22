EXTERNAL Quiz(id_answer)
EXTERNAL getCurrentMoral()
INCLUDE ../Quests/QuestData.ink

#speaker: Soetomo #portrait: soetomo
Karman, bagaimana kita akan menyapa Komandan Jepang?

* [Langsung menuntut dengan nada tinggi]  
    ~ Quiz("4_1")
    -> setelahQuiz4_1

* [Memberi salam militer dan perkenalan tegas]  
    ~ Quiz("4_2")
    -> setelahQuiz4_2

=== setelahQuiz4_1 ===
#speaker:null #portrait: null
(Karman melangkah cepat ke tengah ruangan tanpa basa-basi, lalu bicara keras) 
#speaker:Karman #portrait: karman
Kami datang untuk satu hal: penyerahan senjata. Tidak ada waktu untuk diskusi panjang.  
#speaker: Komandan Jepang #portrait: null  
Nada keras itu tidak pantas dalam perundingan. Jika kalian tidak datang untuk berbicara dengan kepala dingin, maka lebih baik kalian pergi.  
#speaker:null #portrait: null
(Bung Tomo menatap Karman sejenak, wajahnya menegang)
#speaker: Soetomo #portrait: soetomo
Karman... kita di sini bukan untuk cari ribut.  
-> Diplomacy2

=== setelahQuiz4_2 ===
#speaker:null #portrait: null
(Karman berdiri tegap dan memberi salam singkat dengan nada sopan namun jelas) 
#speaker:Karman #portrait: karman
Saya Karman, bersama Bung Tomo dan M. Yasin. Kami datang sebagai wakil pemuda Surabaya, untuk mencari penyelesaian damai.    
#speaker: Komandan Jepang #portrait: null  
Sikap kalian mencerminkan kehormatan. Silakan duduk. Saya akan dengarkan maksud kedatangan ini. 
#speaker:null #portrait: null
(M. Yasin mengangguk pelan pada Karman)
#speaker:M. Yasin #portrait: m_yasin
Pendekatan yang tepat. Kita butuh kepala dingin agar berhasil. 
-> Diplomacy2


=== Diplomacy2 ===
#speaker: Komandan Jepang #portrait: null
Silakan duduk. Sikap ini menunjukkan bahwa kalian datang untuk berbicara.

* [Bersikap tegas namun sopan]  
    ~ Quiz("5_1")
    -> setelahQuiz5_1

* [Mengancam dengan nada keras]  
    ~ Quiz("5_2")
    -> setelahQuiz5_2

=== setelahQuiz5_1 ===
#speaker: M. Yasin #portrait: m_yasin
Kami mewakili rakyat Surabaya yang sedang berjuang mempertahankan kemerdekaan. Kami meminta senjata dan perbekalan diserahkan secara damai, tanpa perlawanan.
#speaker: Komandan Jepang #portrait: null
Tuntutan yang langsung dan jelas. Sikap kalian membuat saya berpikir untuk mendengarkan lebih jauh.
#speaker: null #portrait: null
(Komandan Jepang menyilangkan tangan di depan dada, memperhatikan kalian dengan serius)
-> Diplomacy3

=== setelahQuiz5_2 ===
#speaker: Soetomo #portrait: soetomo
Jika kalian tidak menyerahkan senjata sekarang juga, rakyat Surabaya akan bertindak sendiri!
#speaker: Komandan Jepang #portrait: null
Nada seperti itu hanya akan membawa kita ke jalan buntu.
#speaker: null #portrait: null
(Komandan Jepang terlihat tegang, tangannya perlahan menyentuh gagang pedangnya)
-> Diplomacy3


=== Diplomacy3 ===
#speaker: Komandan Jepang #portrait: null
Permintaan kalian sangat besar. Namun, kami masih memiliki perintah dari atasan.

* [Bernegosiasi dan menawarkan kompromi]  
    ~ Quiz("6_1")
    -> setelahQuiz6_1

* [Menekan dengan lebih agresif]  
    ~ Quiz("6_2")
    -> setelahQuiz6_2

=== setelahQuiz6_1 ===
#speaker: Karman #portrait: null
Kami tidak ingin konflik. Senjata itu untuk mempertahankan rakyat kami dari ancaman luar. Kami tidak akan mengganggu pasukan Jepang yang masih bertugas di sini.
#speaker: Komandan Jepang #portrait: null
Hm... kalian tahu cara berbicara seperti diplomat sejati. Itu sikap yang terhormat.
#speaker: null #portrait: null
(Komandan Jepang menghela napas panjang, mulai melunak)
-> PilihanDiplomasiAkhir

=== setelahQuiz6_2 ===
#speaker: Soetomo #portrait: soetomo
Jika kalian menolak, rakyat Surabaya akan mengambil apa yang mereka butuhkan—dengan paksa jika perlu.
#speaker: Komandan Jepang #portrait: null
Apakah kalian datang membawa ancaman? Ini bukan cara untuk berbicara!
#speaker: null #portrait: null
(Suasana ruangan berubah mencekam. Beberapa perwira Jepang di belakang komandan mulai gelisah)
-> PilihanDiplomasiAkhir


=== PilihanDiplomasiAkhir ===
#speaker:Komandan Jepang #portrait:null
Saya tidak bisa menyerahkan semuanya. Namun, saya bisa memberikan sebagian dari senjata dan perbekalan yang ada di gudang ini. 
Itu adalah tawaran terbaik saya.

* [Tetap Meminta Semua]
    -> PilihTetapMeminta

* [Setuju dengan Sebagian]
    -> PilihSetujuSebagian

=== PilihTetapMeminta ===
~ temp moral = getCurrentMoral()

{ moral >= 50: 
 #speaker: null #portrait: null
    Setelah berpikir panjang, Komandan Jepang akhirnya mengangguk dengan berat hati.
    #speaker:Komandan Jepang #portrait:null
    Baiklah. Semua akan menjadi milik kalian.
    -> EndingSuksesTotal
- else: 
     #speaker: null #portrait: null
    (Komandan Jepang berdiri dengan amarah)
    #speaker:Komandan Jepang #portrait:null
    Tidak ada lagi yang perlu dibicarakan! Keluar dari ruangan ini sekarang!
    -> EndingGagal
}

=== PilihSetujuSebagian ===
~ temp moral = getCurrentMoral()

{ moral >= 50:
 #speaker: null #portrait: null
    Komandan Jepang mengangguk dengan lega.
    #speaker:Komandan Jepang #portrait:null
    Kalian adalah negosiator yang bijaksana. Barang-barang akan diserahkan segera."
    -> EndingSuksesSebagian
- else:
    Komandan Jepang tetap tampak tidak puas.
    #speaker:Komandan Jepang #portrait:null
    Kalian beruntung saya tidak ingin menciptakan kekacauan. Ambil apa yang bisa kalian dapatkan, lalu pergi.
    -> EndingTerpaksa
}

=== EndingSuksesTotal ===
 #speaker: null #portrait: null
Kalian berhasil mendapatkan seluruh senjata dan perbekalan tanpa perlawanan. Ini adalah kemenangan diplomasi yang luar biasa!
~ sukses_total = true
-> END

=== EndingSuksesSebagian ===
 #speaker: null #portrait: null
 Sebagian perbekalan dan senjata kini berada di tangan kalian. Meskipun bukan kemenangan total, ini tetap pencapaian yang besar.
 ~ sukses_sebagian = true
-> END

=== EndingTerpaksa ===
 #speaker: null #portrait: null
Negosiasi berjalan sulit, tetapi setidaknya kalian mendapatkan sesuatu daripada tidak sama sekali. Ini adalah kompromi yang pahit.
~ ending_terpaksa = true
-> END

=== EndingGagal ===
 #speaker: null #portrait: null
Negosiasi gagal total. Tidak ada senjata atau perbekalan yang didapat. Kalian harus mencari cara lain untuk menghadapi situasi ini.
~ ending_terpaksa = true
-> END