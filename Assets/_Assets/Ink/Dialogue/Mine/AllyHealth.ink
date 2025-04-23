INCLUDE ../../Quests/QuestData.ink

#speaker:M.yamin
Kita harus prioritaskan apa?
* [Fokus Kesehatan Pasukan]
    ~ set_health_bonus = true
    okedeh
    -> lanjut
* [Fokus Latihan Pasukan]
    ~ set_damage_bonus = true
    mang eak
    -> lanjut
    
===lanjut===
#speaker:M.yamin
Betul
->END