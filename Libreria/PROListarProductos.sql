USE   [libreria]  
GO  
 / * * * * * *   O b j e c t :     S t o r e d P r o c e d u r e   [ d b o ] . [ P R O L i s t a r P r o d u c t o s ]         S c r i p t   D a t e :   2 5 / 1 1 / 2 0 1 9   1 0 : 4 4 : 1 6   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
   - -   e x e c   [ P R O L i s t a r P r o d u c t o s ]  
 A L T E R   P R O C E D U R E   [ d b o ] . [ P R O L i s t a r P r o d u c t o s ]  
 A S  
 B E G I N  
 	 S E L E C T   S t o c k . I d P r o d u c t o  
 	         , P r o d u c t o . C o d i g o  
 	 	 , P r o d u c t o . N o m b r e  
 	 	 , P r o d u c t o . D e s c r i p c i o n  
 	 	 , P r o d u c t o . F o t o  
 	 	 , S t o c k . C o s t o V e n t a  
 	 	 , I I F ( D e s c u e n t o . P o r c e n t a j e   I S   N U L L ,   S t o c k . C o s t o V e n t a ,   ( S t o c k . C o s t o V e n t a   -   ( S t o c k . C o s t o V e n t a   *   ( D e s c u e n t o . P o r c e n t a j e   /   1 0 0 ) ) ) )   A S   C o b r a r  
 	 F R O M   P r o d u c t o  
 	 I N N E R   J O I N   S t o c k   O N   S t o c k . I d P r o d u c t o   =   P r o d u c t o . I d P r o d u c t o  
 	 L E F T   J O I N   D e s c u e n t o   O N   P r o d u c t o . I d P r o d u c t o   =   D e s c u e n t o . I d P r o d u c t o  
 	 W H E R E   S t o c k . C a n t i d a d   >   0  
 E N D  
 
