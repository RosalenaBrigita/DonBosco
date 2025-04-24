INCLUDE ../../Quests/QuestData.ink

#speaker:Moestopo #audio:null #portrait:moestopo
Apa yang kau butuhkan, Bung?
-> END

===AntarPesan===
#speaker:Moestopo #audio:null #portrait:moestopo
Karman, kau datang tepat waktu. Aku sudah menunggu pesan dari Soengkono. Bagaimana situasi di markas?

#speaker:Karman #audio:null #portrait:karman
Banyak yang bersiap, Bung. Tapi juga banyak yang cemas. Pesawat-pesawat Sekutu mulai menebar ancaman.

#speaker:Moestopo #audio:null #portrait:moestopo
Ya… ini bukan waktu untuk lengah. Tapi juga bukan waktu untuk gegabah. Kita butuh tindakan yang bijak dan cepat.
-> END

===Diplomasi===
#speaker:Moestopo #audio:null #portrait:moestopo
Gudang Don Bosco sekarang dikepung oleh pemuda-pemuda kita. 
Tapi ingat baik-baik, lokasi itu dekat pemukiman penduduk. Kita tidak bisa bertindak gegabah.

* [Apa maksudnya rumit, Bung?]
#speaker:Moestopo #audio:null #portrait:moestopo
    Rumit karena ini bukan cuma soal militer. Di sekitar gudang itu, banyak rakyat sipil. 
    #speaker:Moestopo #audio:null #portrait:moestopo
    Salah langkah, bisa terjadi pembantaian. Kita tidak ingin jadi penjajah di negeri sendiri.
    -> Diplomasi2
* [Kita punya senjata sekarang, kenapa tidak serang saja?]
  #speaker:Moestopo #audio:null #portrait:moestopo
    Itulah yang membuat kita beda, Karman. Kita bukan hanya ingin menang—kita ingin menang dengan bermartabat.
    Jangan biarkan amarah membutakan tujuan kita.
    -> Diplomasi2
* [Gudang DonBosco... penting ya?]
    #speaker:Moestopo #audio:null #portrait:moestopo
    Sangat penting. Di sana tersimpan senjata, makanan, dan perbekalan. Tapi nyawa rakyat sipil jauh lebih penting dari logistik.
    -> Diplomasi2

===Diplomasi2===
#speaker:Soetomo #audio:null #portrait:soetomo
Kita tidak akan menyerbu kali ini. Kita akan berunding. Diplomasi mungkin tidak terdengar heroik, tapi menyelamatkan nyawa adalah kemenangan yang sejati.
* [Kalau begitu... kita harus berunding?]
    #speaker:M. Yasin #audio:null #portrait:m_yasin
    Benar. Tapi ini bukan perundingan biasa. Kita harus tegas tapi sopan. Mereka harus paham bahwa kita tidak main-main.
    -> Diplomasi3
* [Apa Jepang akan menyerah begitu saja?]
    #speaker:M. Yasin #audio:null #portrait:m_yasin
    Tidak ada jaminan. Tapi memberi mereka kesempatan untuk menyerah adalah tanda kekuatan, bukan kelemahan.
    -> Diplomasi3
* [Izinkan saya ikut!]
    #speaker:M. Yasin #audio:null #portrait:m_yasin
    Kau ikut, Karman. Tapi ingat, ini misi kepala dingin. Kalau emosimu menguasai dirimu, lebih baik kau tetap di markas.
    -> Diplomasi3
    
===Diplomasi3===
#speaker:Moestopo #audio:null #portrait:moestopo
Bung Tomo, M. Yasin, dan Karman. Kalian akan jadi utusan kita. Lakukan ini dengan kehormatan. Kalau bisa tanpa setetes darah, maka itulah kemenangan terbesar kita.
* [Saya mengerti. Saya siap belajar dari kalian.]
#speaker:Moestopo #audio:null #portrait:moestopo
Bagus, Karman. Ini bukan hanya tentang bicara—ini tentang menunjukkan bahwa kita punya harga diri.
{quest_MengibarkanBendera_completed == true:
        ->Diplomasi4
    - else:
        ->DiplomasiAkhir
    }
* [Jadi saya hanya ikut mendengarkan?]
#speaker:Moestopo #audio:null #portrait:moestopo
Mendengar adalah awal dari kebijaksanaan. Tapi kau juga akan belajar bicara di waktu yang tepat.
{quest_MengibarkanBendera_completed == true:
        ->Diplomasi4
    - else:
        ->DiplomasiAkhir
    }

==Diplomasi4===
#speaker:Soetomo #audio:null #portrait:soetomo
Sebelum kalian berangkat... ada satu hal yang perlu kita putuskan. Apakah kita akan membawa bendera merah putih dalam misi ini?

* [Ya, harus bawa. Itu lambang kedaulatan kita.]
~ set_bendera = true
    #speaker:Karman #audio:null #portrait:karman
    Tentu, kita harus bawa, Bung. Merah putih bukan hanya kain—itu nyawa bangsa. Kita datang bukan sebagai pengemis damai, tapi sebagai bangsa merdeka yang siap bicara.

    #speaker:M. Yasin #audio:null #portrait:m_yasin
    Hm. Sebuah sikap yang berani. Tapi kau sadar, Karman? Jika kita membawa merah putih dan mereka menembak... itu bukan sekadar penolakan, itu penghinaan terhadap Republik.

    #speaker:Karman #audio:null #portrait:karman
    Dan karena itu... mereka akan berpikir dua kali sebelum menarik pelatuk. Mereka akan tahu siapa yang mereka hadapi—bangsa yang tak lagi dijajah.

    #speaker:Moestopo #audio:null #portrait:moestopo
    Bendera itu akan berdiri... sebagai saksi bahwa kita datang dengan harga diri, bukan ketakutan.

    #speaker:Soetomo #audio:null #portrait:soetomo
    Dan kalau hari ini merah putih berkibar di hadapan mereka tanpa sebutir peluru pun... maka itu adalah kemenangan sejati kita. Bawa bendera itu dengan tegak, Karman.

    -> DiplomasiAkhir

* [Tidak perlu bawa. Cukup sikap kita yang menunjukkan martabat.]
    #speaker:Karman #audio:null #portrait:karman
    Menurutku, kita tidak perlu membawanya kali ini. Biarlah sikap dan kata-kata kita yang bicara. Bendera bisa jadi sasaran—dan aku tak ingin melihat merah putih roboh.

    #speaker:M. Yasin #audio:null #portrait:m_yasin
    Kau benar. Terkadang, kehormatan tak perlu ditunjukkan dengan simbol, tapi dengan keberanian untuk berdiri tanpa perlindungan.

    #speaker:Moestopo #audio:null #portrait:moestopo
    Tapi ingat, Karman. Merah putih bukan sekadar lambang perang. Ia lambang hidup kita bersama. Tanpanya, kita hanya sekelompok pemuda tanpa arah.

    #speaker:Soetomo #audio:null #portrait:soetomo
    Hm, pilihan yang berani... dan berisiko. Tapi kalau kau yakin, kami percaya padamu. Pastikan kata-katamu membawa nyala api yang sama seperti merah putih itu.

    -> DiplomasiAkhir

===DiplomasiAkhir===
#speaker:Moestopo #audio:null #portrait:moestopo
Karman, jalan yang kau pilih bukan mudah. Tapi setiap langkahmu hari ini akan tercatat dalam ingatan bangsa ini.

#speaker:M. Yasin #audio:null #portrait:m_yasin
Perhatikan wajah-wajah mereka. Dengarkan baik-baik. Satu kata bisa menyelamatkan, satu gerak bisa menyulut api. Bicaralah dengan kepala dingin, hati panas.

#speaker:Soetomo #audio:null #portrait:soetomo
Dan kalau saatnya tiba, jangan ragu menunjukkan siapa kita.

#speaker:Karman #audio:null #portrait:karman
Saya mengerti. Saya akan berdiri di hadapan mereka, bukan sebagai satu orang... tapi sebagai wakil dari seluruh pemuda Surabaya.

-> END



