<?xml version="1.0" encoding="UTF-8"?>
<tileset version="1.2" tiledversion="1.3.4" name="Collisions" tilewidth="8" tileheight="8" tilecount="5" columns="0">
 <tile id="0">
  <image width="8" height="8" source="../_MapCollisions/Full.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="0">
    <polygon points="0,0 0,8 8,8 8,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="1">
  <image width="8" height="8" source="../_MapCollisions/TopLeft.png"/>
  <objectgroup draworder="index">
   <object id="2" x="0" y="0">
    <polygon points="0,0 0,8 1,8 8,1 8,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="2">
  <image width="8" height="8" source="../_MapCollisions/TopRight.png"/>
  <objectgroup draworder="index">
   <object id="2" x="8" y="8">
    <polygon points="0,0 -1,0 -8,-7 -8,-8 0,-8"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="3">
  <image width="8" height="8" source="../_MapCollisions/BottomLeft.png"/>
  <objectgroup draworder="index">
   <object id="2" x="8" y="8">
    <polygon points="0,0 -8,0 -8,-8 -7,-8 0,-1"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="4">
  <image width="8" height="8" source="../_MapCollisions/BottomRight.png"/>
  <objectgroup draworder="index">
   <object id="2" x="8" y="8">
    <polygon points="0,0 -8,0 -8,-1 -1,-8 0,-8"/>
   </object>
  </objectgroup>
 </tile>
</tileset>
