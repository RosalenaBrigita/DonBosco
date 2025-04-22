INCLUDE ../../Quests/QuestData.ink

#speaker:null
(Ketiganya melangkah keluar dari gudang. Matahari siang menyambut, dan puluhan pemuda berkumpul di luar menunggu hasil perundingan.)

#speaker:Soetomo
Mereka menunggu jawaban, Yasin… Karman. Kau yang memimpin pembicaraan tadi. Katakan pada mereka.

{ sukses_total:
    #speaker:Karman
    Kita berhasil. Seluruh isi gudang—senjata, amunisi, bahkan logistik—akan diserahkan kepada kita. Tanpa setetes darah pun tumpah.

    #speaker:M.Yasin
    Diplomasi kita membuahkan hasil luar biasa. Rakyat Surabaya kini punya senjata untuk menghadapi ancaman yang lebih besar.

    #speaker:Soetomo
    Kemenangan ini… adalah bukti bahwa kata-kata pun bisa sekuat peluru, bila digunakan dengan bijak.
- else:
    { sukses_sebagian:
        #speaker:Karman
        Tidak semuanya… tapi sebagian besar senjata akan kita bawa keluar hari ini. Komandan Jepang setuju, dengan syarat tertentu.

        #speaker:M.Yasin
        Ini mungkin bukan kemenangan penuh, tapi cukup untuk memperkuat barisan kita. Kita punya amunisi… dan waktu.

        #speaker:Soetomo
        Kita mengambil apa yang kita bisa. Dan rakyat akan melihat, perjuangan tidak pernah berhenti.
    - else:
        { ending_terpaksa:
            #speaker:Karman
            Hasilnya… tidak seperti yang kita harapkan. Mereka hanya menyerahkan sebagian kecil senjata, dan itu pun dengan sikap terpaksa.

            #speaker:M.Yasin
            Tapi lebih baik kita pulang membawa sesuatu… daripada tidak membawa apa-apa. Kita tidak menyerah. Kita akan cari jalan lain.

            #speaker:Soetomo
            Ini adalah kenyataan diplomasi. Kadang, bukan tentang kemenangan… tapi tentang bertahan hari ini, agar bisa berjuang esok.
        - else:
            #speaker:Karman
            Maafkan kami… Negosiasi gagal. Tidak ada senjata, tidak ada logistik. Mereka menutup pintu rapat-rapat.

            #speaker:M.Yasin
            Tapi ini bukan akhir. Kita tahu di mana mereka menyimpan kekuatan. Dan kita tahu siapa yang siap berjuang.

            #speaker:Soetomo
            Jangan patah semangat, anak-anak Surabaya. Gagal hari ini bukan berarti kalah selamanya.
        }
    }
}

#speaker:null
(Suara riuh pemuda membesar. Beberapa bersorak, beberapa terdiam, merenung. Tapi di mata mereka… tidak ada ketakutan.)

#speaker:Karman
Apapun hasilnya… kita tetap berdiri di sini. Bersama. Dan perjuangan belum berakhir.

-> END
