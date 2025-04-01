EXTERNAL Quiz(id_answer)
EXTERNAL getCurrentMoral()

{getCurrentMoral()}

#speaker:Bung Tomo #portrait:null
"Karman, bagaimana kita akan masuk ke ruangan negosiasi?"

* [Membuka pintu dengan kasar]
    ~ Quiz("6_1")
    Karman memberi isyarat kepada M. Yasin untuk membuka pintu dengan keras. Suara bantingan pintu menggema, membuat suasana langsung memanas.
    -> Diplomacy2

* [Menghormati dan ketuk pintu]
    ~ Quiz("6_2")
    Karman menyarankan untuk mengetuk pintu dengan tenang. M. Yasin mengetuk pintu tiga kali sebelum melangkah masuk dengan sikap penuh keyakinan.
    -> Diplomacy2

=== Diplomacy2 ===
Komandan Jepang melihat ke arah kalian dengan ekspresi serius.

#speaker:Komandan Jepang #portrait:null
"Silakan duduk. Sikap ini menunjukkan bahwa kalian datang untuk berbicara."

* [Bersikap tegas namun sopan]
    ~ Quiz("7_1")
    M. Yasin menjelaskan tuntutan dengan nada tegas namun tetap menghormati posisi lawan. Komandan Jepang mempertimbangkan opsi ini dengan serius.
    -> Diplomacy3

* [Mengancam dengan nada keras]
    ~ Quiz("7_2")
    Bung Tomo menggebrak meja dan berbicara dengan nada keras, menuntut semua senjata segera diserahkan.
    Komandan Jepang tampak tidak senang, dan suasana semakin tegang.
    -> Diplomacy3

=== Diplomacy3 ===
#speaker:Komandan Jepang #portrait:null
"Permintaan kalian sangat besar. Namun, kami masih memiliki perintah dari atasan."

* [Bernegosiasi dan menawarkan kompromi]
    ~ Quiz("8_1")
    "Kami hanya meminta persenjataan agar rakyat kami dapat melindungi diri dari ancaman luar. Kami tidak akan membahayakan pasukan Jepang yang ada di sini."
    Komandan Jepang mulai melunak.
    -> PilihanDiplomasiAkhir

* [Menekan dengan lebih agresif]
    ~ Quiz("8_2")
    "Jika kalian menolak, rakyat Surabaya akan mengambilnya dengan cara mereka sendiri!"
    Komandan Jepang terlihat semakin defensif.
    -> PilihanDiplomasiAkhir

=== PilihanDiplomasiAkhir ===
#speaker:Komandan Bataliyon Jepang #portrait:null
"Saya tidak bisa menyerahkan semuanya. Namun, saya bisa memberikan sebagian dari senjata dan perbekalan yang ada di gudang ini. Itu adalah tawaran terbaik saya."
* [Tetap Meminta Semua]
    -> PilihTetapMeminta

* [Setuju dengan Sebagian]
    -> PilihSetujuSebagian

=== PilihTetapMeminta ===
~ temp moral = getCurrentMoral()

{ moral >= 50: 
    Narasi: "Setelah berpikir panjang, Komandan Bataliyon akhirnya mengangguk dengan berat hati."
    #speaker:Komandan Bataliyon Jepang #portrait:null
    "Baiklah. Semua akan menjadi milik kalian."
    -> EndingSuksesTotal
- else: 
    Narasi: "Komandan Bataliyon berdiri dengan amarah."
    #speaker:Komandan Bataliyon Jepang #portrait:null
    "Tidak ada lagi yang perlu dibicarakan! Keluar dari ruangan ini sekarang!"
    -> EndingGagal
}

=== PilihSetujuSebagian ===
~ temp moral = getCurrentMoral()

{ moral >= 50:
    Narasi: "Komandan Bataliyon mengangguk dengan lega."
    #speaker:Komandan Bataliyon Jepang #portrait:null
    "Kalian adalah negosiator yang bijaksana. Barang-barang akan diserahkan segera."
    -> EndingSuksesSebagian
- else:
    Narasi: "Komandan Bataliyon tetap tampak tidak puas."
    #speaker:Komandan Bataliyon Jepang #portrait:null
    "Kalian beruntung saya tidak ingin menciptakan kekacauan. Ambil apa yang bisa kalian dapatkan, lalu pergi."
    -> EndingTerpaksa
}

=== EndingSuksesTotal ===
Narasi: "Kalian berhasil mendapatkan seluruh senjata dan perbekalan tanpa perlawanan. Ini adalah kemenangan diplomasi yang luar biasa!"
-> END

=== EndingSuksesSebagian ===
Narasi: "Sebagian perbekalan dan senjata kini berada di tangan kalian. Meskipun bukan kemenangan total, ini tetap pencapaian yang besar."
-> END

=== EndingTerpaksa ===
Narasi: "Negosiasi berjalan sulit, tetapi setidaknya kalian mendapatkan sesuatu daripada tidak sama sekali. Ini adalah kompromi yang pahit."
-> END

=== EndingGagal ===
Narasi: "Negosiasi gagal total. Tidak ada senjata atau perbekalan yang didapat. Kalian harus mencari cara lain untuk menghadapi situasi ini."
-> END


=== Ending_Tragis ===
Komandan Jepang menolak tuntutan dengan keras, dan salah satu prajurit BKR kehilangan kesabaran, menyebabkan bentrokan senjata terjadi di dalam ruangan.
-> END
