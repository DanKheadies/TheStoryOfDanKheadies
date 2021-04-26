<?xml version="1.0" encoding="UTF-8"?>
<tileset name="Collisions" tilewidth="8" tileheight="8" tilecount="5" columns="0">
 <grid orientation="orthogonal" width="1" height="1"/>
 <tile id="0">
  <image width="8" height="8" source="BottomLeft.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="8">
    <polygon points="0,0 0,-8 8,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="1">
  <image width="8" height="8" source="BottomRight.png"/>
  <objectgroup draworder="index">
   <object id="1" x="8" y="8">
    <polygon points="0,0 0,-8 -8,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="2">
  <image width="8" height="8" source="Full.png"/>
  <objectgroup draworder="index">
   <object id="10" x="0" y="0" width="8" height="8"/>
  </objectgroup>
 </tile>
 <tile id="3">
  <image width="8" height="8" source="TopLeft.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="0">
    <polyline points="0,0 0,8 8,0 0,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="4">
  <image width="8" height="8" source="TopRight.png"/>
  <objectgroup draworder="index">
   <object id="1" x="8" y="8">
    <polygon points="0,0 -8,-8 0,-8"/>
   </object>
  </objectgroup>
 </tile>
</tileset>
