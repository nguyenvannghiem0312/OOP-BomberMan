# OOP-BomberMan
BomberMan Game sử dụng Unity engine

Chương trình sử dụng ngôn ngữ C# kết hợp với Unity engine mô phỏng lại game Bomberman nổi tiếng theo lối chơi tính điểm.

![This is an image](/item.png)

## Các đối tượng trong game

Nếu các bạn đã từng chơi tự game Bomb quen thuộc thì hẳn sẽ không xa lạ với những đối tượng này:

* _Player_: là nhân vật chính của trò chơi. Người chơi có thể di chuyển theo 4 hướng trái/phải/lên/xuống và đặt _Bomb_ để tiêu diệt _Enemy_.
* _Enemy_: là các đối tượng mà người chơi cần phải tiêu diệt. Tiêu diệt _Enemy_ sẽ nhận được điểm tương ứng, khi _Enemy_ bị tiêu diệt, 1 con _Enemy_ khác sẽ được sinh ra ngẫu nhiên và khi điểm của người chơi càng cao thì càng nhiều _Enemy_ được sinh ra.
* _Bomb_: là đối tượng mà người chơi sẽ đặt và kích hoạt bomb nổ sau 1 khoảng thời gian nhất định. Khi bomb nổ đối tượng _Flame_ sẽ nổ ra theo 4 hướng với bán kính nhất định.
* _Flame_: là đối tượng được sinh ra khi _Bomb_ nổ. _Flame_ có thể phá hủy các _Brick_ và tiêu diệt lẫn _Player_ cả _Enemy_.
* _Brick_: là đối tượng xuất hiện trên bản đồ mà ở đó người chơi hay quái không thể di chuyển, đặt bomb vào nhưng có thể bị phá hủy bởi _Bomb_ và có xác suất xuất hện các Item khi đối tượng được phá hủy.
* _Item_: là đối tượng được xuất hiện ngẫu nhiên khi người chơi phá hủy các _Brick_. 
* Ngoài ra có 1 số đối tượng tĩnh khác như: _Grass_, _Wall_.

## Thông tin các loại _Enemy_

Khi _Player_ tiêu diệt các _Enemy_ thì sẽ nhận được điểm tương ứng dựa theo tốc độ của _Enemy_, tốc độ càng cao điểm càng nhiều:

* _Bat_: là những con dơi có tốc độ là 2 (_Player_ có tốc độ ban đầu là 5) tương ứng với 10 điểm.
* _Ground Dragon_: là những con rồng đất có tốc độ cao hơn là 5 ngang với tốc độ _Player_ ban đâu tương ứng với 20 điểm.
* _Fire Dragon_: rồng lửa có tốc độ cao hơn là 7 với mức điểm là 30 điểm.
* _Sonic_: là _Enemy_ có tốc độ cao là 12 với mức điểm 40 điểm.
* _SuperSonic_: giống với _Sonic_ những có hoạt ảnh khác và tốc độ cao nhất là 15 tương ứng mức điểm 50.

## Thông tin các loại _Item_

Khi phá hủy các _Brick_ sẽ có xác suất ngẫu nhiên xuất hiện các _Item_, khi _Player_ nhặt các _Item_ sẽ giúp tăng thuộc tính tương ứng:

* _HPItem_: tăng 1 HP cho _Player_.
* _SpeedItem_: tăng 1 speed cho _Player_.
* _BombItem_: tăng 1 quả bomb cho _Player_.
* _FlmaeItem_: tăng bán kính bomb nổ lên 1.

## Mô tả Game Play

Nhiệm vụ của mỗi người chơi là di chuyển và đặt bomb tiêu diệt nhiều quái nhất có thể. Mỗi quái sẽ nhân được mức điểm tương ứng. Phá hủy các viên gạch sẽ có xác suất nhận được các item, các item sẽ giúp tăng chỉ cho người chơi do vậy giai đoạn đầu game hãy tập trung phá các viên gạch để nâng cấp _Player_. Người chơi sẽ bị mất máu khi va chạm với _Enemy_ hoặc các _Flame_ do _Bomb_ nổ tạo ra. Các _Enemy_ cũng có thể bị tiêu diệt khi chúng va chạm vào nhau nhưng sẽ không được tính điểm. Khi mỗi _Enemy_ bị tiêu diệt sẽ có ngẫu nhiên _Enemy_ khác xuất hiện tại vị trí ngẫu nhiên do vậy bạn hãy cẩn thân nếu không không kịp trở tay. Khi điểm của bạn càng cao thì số lượng _Enemy_ xuất hiện càng nhiều.
