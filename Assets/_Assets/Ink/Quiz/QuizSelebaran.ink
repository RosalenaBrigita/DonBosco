EXTERNAL Quiz(id_answer)
INCLUDE ../Quests/QuestData.ink

#speaker:Moestopo #audio:moestopo #portrait:moestopo
Karman! Kau datang di waktu yang tepat. Kami baru saja menerima kabar tentang selebaran yang dijatuhkan dari udara...  
~ set_quiz = true
Aku belum tahu pasti isinya. Apa kau melihatnya sendiri?

* [Isinya memerintahkan rakyat untuk menyerah.]
    ~ Quiz("2_1")
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Saya lihat sendiri, Bung. Selebaran itu memerintahkan rakyat untuk tunduk... menyerah pada Sekutu.
    ~ set_quiz = false
    #speaker:Moestopo #audio:moestopo #portrait:moestopo
    Hm... bukan itu. Coba dengarkan lebih baik isi selebaran tadi.
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Kau benar, Bung. Selebaran itu menyatakan sekutu akan datang.
    #speaker:Moestopo #audio:moestopo #portrait:moestopo
    Kalau mereka datang, maka waktunya kita bangkit.  
    Kita tak bisa tunggu lebih lama. Gudang Don Bosco harus jadi milik kita sebelum mereka sempat menginjak tanah ini.
    -> Lanjut

* [Mereka bilang Sekutu akan datang.]
    ~ Quiz("2_2")
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Selebaran itu penuh ancaman. Sekutu akan datang, katanya. Mereka ingin kita gentar sebelum peluru pertama ditembakkan.
    ~ set_quiz = false
    #speaker:Moestopo #audio:moestopo #portrait:moestopo
    Kalau mereka datang, maka waktunya kita bangkit.  
    Kita tak bisa tunggu lebih lama. Gudang Don Bosco harus jadi milik kita sebelum mereka sempat menginjak tanah ini.

    -> Lanjut

===Lanjut===
#speaker:Moestopo #audio:moestopo #portrait:moestopo
Gudang Don Bosco bukan sekadar gudang. Itu adalah pusat kekuatan Jepang terakhir di Surabaya.  
Senjata, amunisi, makanan—semuanya ditumpuk di sana. Jika kita berhasil menguasainya…  
bukan hanya logistik yang kita rebut, tapi juga semangat juang ribuan rakyat.

* [Tapi... kenapa bukan tempat lain?]
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Bung, saya tidak meragukan niat kita... tapi apakah menyerang Don Bosco bukan tindakan yang terlalu berisiko?

    #speaker:Moestopo #audio:moestopo #portrait:moestopo
    Karena Gudang Don Bosco bukan hanya tentang persenjataan.  
    Ini adalah simbol. Kalau kita bisa menaklukkan markas terbesar mereka...  
    kita menyalakan api perlawanan di seluruh penjuru Surabaya.
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Saya paham sekarang, Gudang Don Bosco bukan sekadar tempat... itu simbol perjuangan.

-> Lanjut2

===Lanjut2====
#speaker:Moestopo #audio:moestopo #portrait:moestopo
Kita sudah berjuang dengan bambu runcing dan senjata seadanya. Tapi Sekutu bukan penjajah biasa.  
Gudang Don Bosco menyimpan senjata berat, granat, bahkan pelontar api. Jika itu jatuh ke tangan rakyat—kita ubah peta pertempuran.

#speaker:Karman #audio:animal_crossing_mid #portrait:karman
Saya mengerti. Bukan cuma senjata, ini tentang membuktikan bahwa kita bisa melawan.

#speaker:Moestopo #audio:moestopo #portrait:moestopo
Benar. Tapi untuk bisa menyerbu Don Bosco, kita harus punya rencana.  
Dan kita harus belajar dari dua operasi sebelumnya: penyerbuan di Gudang Sambongan dan Kaliasin.

* [Apa yang terjadi di dua tempat itu?]
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Saya belum tahu detailnya, Bung. Apa yang sebenarnya terjadi di sana?

    #speaker:Moestopo #audio:moestopo #portrait:moestopo
    Di Sambongan, pemuda berhasil menyusup lewat saluran air, menyergap Jepang dari dalam.  
    Di Kaliasin, mereka menyamar sebagai pekerja gudang—dan dalam hitungan jam, seluruh persenjataan jatuh ke tangan kita.  
    Tanpa korban jiwa besar. Itu keberhasilan yang harus kita ulangi.
    ->Lanjut3

* [Siapa yang merancang operasi itu?]
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Siapa yang mengatur semua itu, Bung? Mereka pasti punya strategi luar biasa.

    #speaker:Moestopo #audio:moestopo #portrait:moestopo
    Polisi Istimewa. Dan satu nama yang wajib kau temui: M. Yasin.  
    Dia bukan hanya saksi, tapi otak di balik strategi. Kalau kita ingin berhasil di Don Bosco, kita perlu tahu pikirannya.
    -> Lanjut3

===Lanjut3===
#speaker:Karman #audio:animal_crossing_mid #portrait:karman
Di mana saya bisa menemuinya?

#speaker:Moestopo #audio:moestopo #portrait:moestopo
Datanglah ke Kantor Polisi Istimewa.  
Tanyakan langsung tentang operasi Sambongan dan Kaliasin. Dengarkan baik-baik... lalu kita susun rencana penyerangan besar.

* [Baik, Bung. Saya akan segera ke sana.]
    #speaker:Karman #audio:animal_crossing_mid #portrait:karman
    Saya akan temui M. Yasin dan pelajari semua yang bisa membantu kita.

#speaker:Moestopo #audio:moestopo #portrait:moestopo
Bagus, Karman. Langkah ini akan menentukan arah perjuangan kita. Jangan tunda, Surabaya menunggu keberanianmu.  
Ini lebih dari sekadar tugas, ini langkah menuju kemerdekaan sejati. Bawalah semangat Surabaya bersamamu, Karman.
-> END
