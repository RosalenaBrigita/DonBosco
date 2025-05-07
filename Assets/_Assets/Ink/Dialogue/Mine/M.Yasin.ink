INCLUDE ../../Quests/QuestData.ink

#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Kau butuh sesuatu?
-> END

===AjakBicara===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Karman... Terima kasih telah datang tepat waktu. 
Kami telah mendengar sepak terjangmu selama ini, dan kami percaya, darah juangmu akan menjadi nyala api dalam misi ini.

Target kita jelas—Gudang Don Bosco. Tempat itu ibarat jantung bagi pasukan Jepang di Surabaya. 
Jika berhasil kita rebut, maka bukan hanya senjata yang kita dapatkan, tapi juga harapan dan semangat bagi seluruh rakyat yang tengah berjuang.

* [Apa yang harus saya lakukan, Pak Yasin?] -> BriefingTugas

===BriefingTugas===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Kau akan tergabung dalam tim serbu. Kita ada tiga puluh orang—sebagian pemuda seperti dirimu, sebagian lagi bekas tentara KNIL dan laskar rakyat.

Dibagi jadi dua tim: tim serbu dan tim logistik. Tim serbu akan bergerak cepat dan menyusup ke dalam gudang. 
Tim logistik akan bertugas mengangkut senjata dan perlengkapan yang berhasil kita rampas ke tempat aman.

Sebelum kita susun strategi penyerangan secara rinci, saya ingin kau memilih mana yang akan kau awasi secara langsung dalam persiapan malam ini.

* [Saya akan fokus melatih pasukan agar siap bertempur.] -> PersiapanLatihan
* [Saya akan bantu memastikan kondisi kesehatan dan logistik pasukan.] -> PersiapanKesehatan

===PersiapanLatihan===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Baik. Latihan senjata dan simulasi penyusupan akan sangat menentukan. 
Pastikan mereka tahu kapan harus bergerak dan kapan harus diam. Gunakan waktu sebaik mungkin.
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Saya percayakan itu padamu.
~ start_quest = "LatihanMenembak"
-> END

===PersiapanKesehatan===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Langkah bijak. Banyak dari mereka kelelahan, ada pula yang terluka dari bentrokan sebelumnya. 
Berikan obat, perban, dan semangat. Kondisi mereka bisa menentukan hasil malam ini. Saya percayakan itu padamu.
~ start_quest = "FokusKesehatan"
-> END

===AfterBriefing===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Ada apa Karman? Bukankah kau seharusnya mengecek kondisi para pemuda di depan? 
#speaker:Karman #audio:animal_crossing_mid #portrait:karman
Aku hanya takut mengganggu mereka, Pak.
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Kalau begitu, pastikan kau membantu mereka. Mereka pasti akan menerima mu dengan baik.
->END

===SetelahPersiapanMenembak===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Ah, Karman. Bagaimana latihan tadi?
#speaker:Karman #audio:animal_crossing_mid #portrait:karmann
Kemajuan cukup baik, Pak. Tembakan mereka sudah lebih terarah, tapi masih perlu latihan gerak sambil menembak.
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Sebelum kita susun strategi akhir, ada hal yang perlu kau tahu.
~ set_damage_bonus = true
-> PembahasanLanjutan

===LaporKeMYasin===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Karman. Kondisi pasukan bagaimana?

#speaker:Karman #audio:animal_crossing_mid #portrait:karman
Luka-luka kecil sudah diobati, Pak. Mereka juga baru makan. Tapi...
Beberapa masih ragu. Pemuda tadi bilang, dia khawatir ini bisa jadi makan terakhir mereka.

#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Itu wajar. Kau sudah beri mereka kehangatan. Sekarang... apa kau juga punya keraguan?
#speaker:Karman #audio:animal_crossing_mid #portrait:karman
Tidak, Pak. Tapi... bisakah kita pastikan mereka semua pulang? Setidaknya, pulang dengan cerita.
#speaker:M.Yasin #audio:null #portrait:m_yasin
(meletakkan tangan di bahu Karman)
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Itulah mengapa kau kutugaskan di logistik. Kau ingat mereka bukan sekadar angka.
Besok, ceritakan pada mereka tentang malam ini-bagaimana kau mengantarkan nasi dan harapan.
#speaker:Karman #audio:animal_crossing_mid #portrait:karman
Siap, Pak.
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Sebelum kita susun strategi akhir, ada hal yang perlu kau tahu.
~ set_health_bonus = true
-> PembahasanLanjutan

===PembahasanLanjutan===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Kita tidak bisa menyusun serangan besar-besaran layaknya tentara resmi. Maka kita bertumpu pada dua hal: kejutan dan kecepatan.

Kita akan menyelinap diam-diam dan menyerang ketika mereka sedang lengah. Serangan kita akan berlangsung cepat, tak lebih dari satu jam. 
Lewat dari itu, bala bantuan mereka bisa tiba, dan kita bisa terkepung.
Pemuda-pemuda dari berbagai kampung di Surabaya sudah kita kumpulkan. 
Mereka penuh semangat, apalagi setelah mendengar seruan Bung Tomo di radio yang membakar semangat rakyat.
Kita hanya membawa senjata ringan: karabin tua, granat tangan buatan rakyat, dan pisau bayonet. 
Dua truk akan ikut dari belakang, menunggu aba-aba untuk masuk dan mengangkut apa pun yang bisa dibawa.

* [Bagaimana soal penjagaan musuh?] -> InfoPenjagaan

===InfoPenjagaan===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin  
Dari informasi yang kita kumpulkan dari warga sekitar, Gudang Don Bosco di Sawahan dijaga oleh satu peleton Heiho Jepang.  
Banyak dari mereka—karyawan sipil, tukang becak, bahkan ibu warung—membantu kita mengamati pergerakan musuh.  
Kadang cuma dari obrolan warung, atau dari arah tatapan penjaga.  
Kalau mereka ngedip sedikit aja pas ganti shift, warga kita langsung catat.  
Sekilas tampak sepele... tapi di medan seperti ini, satu detik lengah bisa jadi pintu masuk.

* [Luar biasa... rakyat benar-benar mata dan telinga kita]  
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman  
    Mereka mungkin tak angkat senjata, tapi keberanian mereka tak kalah dari kita di garis depan.
    -> lanjut
    
* [Berarti kita tak boleh menyia-nyiakan kepercayaan ini]  
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman  
    Kalau rakyat sudah bantu sejauh ini... maka kita harus pastikan perjuangan ini tak sia-sia.
    -> lanjut
    
===lanjut==
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin 
Namun, kita belum punya data lengkap tentang pola mereka.
Dari laporan yang dikirim intel kita, mereka sedang dalam masa transisi jaga.
Beberapa pasukan Heiho Jepang mulai direlokasi karena kekalahan di front Pasifik.
Mereka mulai lengah. Mengira rakyat Surabaya tak punya daya. Itu kesombongan mereka yang bisa jadi celah kita.
Karman, guru-guru dari sekolah teknik Don Bosco, termasuk Subianto dan Mamahit, akan ikut turun tangan.
Mereka memimpin pengorganisasian para pemuda, membantu menyiapkan logistik dan rencana serangan.
Apakah kalian mendapat suatu informasi yang berguna dalam misi ini?
#speaker:Subianto #audio:subianto #portrait:subianto
Aku pernah menyusup ke area sekitar gudang itu. Penjagaan di malam hari berkurang. 
Pos jaga sering kali kosong selama beberapa menit karena pergantian shift. Banyak dari mereka terlalu percaya diri.

* [Tapi jika mereka bersiaga?] -> WaktuTerbaik

===WaktuTerbaik===
#speaker:Subianto #audio:subianto #portrait:subianto
Tak ada jaminan dalam perang, Karman. Tapi kita harus memanfaatkan kebiasaan musuh.

Biasanya selepas tengah malam, mereka mulai lengah. Beberapa bahkan tertidur di pos jaga. 
Saat itulah kita menyerang—bagaikan badai dalam senyap.

#speaker:Mamahit #audio:mamahit #portrait:mamahit
Logistik sudah kami atur. Bahan bakar truk penuh, rute pelarian disiapkan, dan para supir sudah paham tugas mereka.
Bantuan moral dari Bung Tomo juga telah membakar semangat kita.
Tapi saat evakuasi nanti, kita butuh satu orang untuk memastikan tak ada senjata, amunisi, atau dokumen penting yang tertinggal.

* [Saya bisa bantu mengatur logistik juga, Pak?] -> FokusTugas

===FokusTugas===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Tidak, Karman. Fokusmu tetap di garis depan. Kau bagian dari tim serbu, dan kami butuh pemimpin muda yang berani di sana.
Aku sendiri, M. Yasin, sebagai pemimpin Polisi Istimewa Surabaya, telah ditunjuk sebagai komandan dalam penyerangan ini.
Serahkan soal logistik pada kami. Mamahit dan timnya sudah hafal rute dan metode pengangkutan. 
Kau bantu kami dengan membuka jalan, dan jika memungkinkan, kawal mereka saat kembali.

#speaker:Mamahit #audio:mamahit #portrait:mamahit
Benar itu. Kami akan berada beberapa menit di belakangmu. Begitu gudang dikuasai, kami masuk cepat, ambil semua yang bisa diangkut.
Termasuk senapan, peluru, dokumen, bahkan perlengkapan medis kalau ada.

#speaker:Mamahit #audio:mamahit #portrait:mamahit
Tugasmu memastikan jalur masuk dan keluar aman. Jika kau berhasil, kami bisa bergerak tanpa suara dan tanpa darah.
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Kita akan berkumpul di sekitar kompleks Gudang Don Bosco pukul dua puluh dua. 
Gunakan pakaian gelap, hindari suara, dan pastikan senjatamu tidak jatuh atau berbunyi saat bergerak.

Kita harus keluar dari area itu sebelum fajar menyingsing. Jika kita gagal, bukan hanya senjata yang hilang, tapi mungkin nyawa kita juga.

* [Dimengerti, Pak. Saya siap.] -> PenutupBriefing

===PenutupBriefing===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Karman... Kau bukan lagi sekadar pemuda biasa. Kau bagian dari arus sejarah. 
Jika kita berhasil malam ini, maka anak cucu kita akan mengingat bahwa kita pernah melawan—dengan darah dan nyawa.
Berjuanglah bukan untuk nama, tapi untuk tanah air. Indonesia belum merdeka sepenuhnya...
Ini saatnya. Serang mendadak dan jangan beri mereka waktu untuk bersiap. 
Pemuda-pemuda kita sudah di posisi. Kau yang memimpin serangan ini, Karman.

-> PilihStrategiAwal

===PilihStrategiAwal===
* [Kita serang dari belakang]
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Kita serang dari belakang. Itu jalur yang paling aman.
    ~ start_quest = "SerangBelakang"
    ~ questBelakang = true
    -> END

* [Tidak, kita serang langsung dari depan!]
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Tidak, kita serang langsung dari depan! Ini akan menunjukkan keberanian kita.
    ~ start_quest = "SerangDepan"
    ~ questDepan = true
    -> END

* [Kita tunggu sampai pagi untuk melihat situasi lebih jelas.]
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Kita tunggu sampai pagi untuk melihat situasi lebih jelas.

    #speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
    Kau yakin? Tapi pemuda kita sudah siap mengintai.

    -> KonfirmasiTungguPagi

===KonfirmasiTungguPagi===
* [Ya, kita tunggu sampai pagi.]
    ~ start_quest = "TungguPagi"
    ~ tungguPagi = true
    -> END

* [Tidak, saya ubah rencana.] -> PilihStrategiTanpaTunggu

===PilihStrategiTanpaTunggu===
* [Kita serang dari belakang]
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Kita serang dari belakang. Itu jalur yang paling aman.
    ~ start_quest = "SerangBelakang"
    ~ questBelakang = true
    -> END

* [Tidak, kita serang langsung dari depan!]
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Tidak, kita serang langsung dari depan! Ini akan menunjukkan keberanian kita.
    ~ start_quest = "SerangDepan"
    ~ questDepan = true
    -> END

===AfterStrategi===
#speaker:M.Yasin #audio:m_yasin #portrait:m_yasin
Segeralah pergi ke Donbosco dan jalankan strategi yang telah kita susun.
->END