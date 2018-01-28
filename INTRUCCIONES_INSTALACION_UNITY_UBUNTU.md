FECHA: ~~20/10/17~~ 28/01/17

1º. Instalar Unity. Puedes obtener el paquete .deb en el siguiente enlace:
https://forum.unity.com/threads/unity-on-linux-release-notes-and-known-issues.350256/

2º. Instalar monodevelop. Aunque parezca que Unity los instala por śi mismo, esto no es así y es preciso instalarlo por nuestra cuenta:

[enlace referencia](https://forum.unity.com/threads/how-to-install-unity-and-monodevelop-on-ubuntu-16-04-linux.485113/)

#Librerias C#
[Instalar desde la página oficial](http://www.mono-project.com/download/#download-lin)
O instalar directamente: sudo apt-get install mono-devel (No recomendado).

#GUI
sudo apt-get install monodevelop

#Para abrir los scripts directamente en monodevelop desde Unity3D (opcional)
sudo apt-get install realpath ()

3º. Si al compilar los scripts da problemas en monodevelop, desplegar el arbol del submenú de la izquierda con el árbol del proyecto, click derecho en el nodo que cuelga directamente de la raíz (Assembly-CSharp), Opciones, Build, General, y cambiar el valor de Target framework a "Mono / .NET 4.5" o uno superior (Los errores que he tenido han sido debido a que este valor estaba puesto a .Net 3.5).
