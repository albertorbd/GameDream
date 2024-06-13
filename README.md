<h1 font-size: 26px >GameDream</h1>
<p font-size: 20px> Gamedream es una aplicación de consola programada en dotnet.</p>
<p font-size: 20px> Consta de una zona pública que te permite <strong>registrarte</strong>, <strong>iniciar sesión</strong> y meterte en la <strong>admin zone</strong> para hacer pruebas.</p>
<p font-size: 20px> En la Admin Zone podrás ver la lista de usuarios registrados, videojuegos, crear un videojuego, modificarlo y borrarlo.</p>
<p font-size: 20px> Una vez te registres o inicies sesión te meterás en la <strong>zona privada</strong> donde verás tus datos y podrás depositar o retirar dinero, comprar videojuegos y ver tu lista de operaciones y videojuegos en posesión. También podrás modificar tu email y contraseña y borrar tu cuenta.</p>

<h2 font-size: 24px;>Docker</h2>
<p font-size: 20px>Puedes descargarte la imagen desde dockerhub en tu ordenador con este comando docker pull <strong>alber965/gamedream:latest</strong></p>
<p font-size: 20px>Si quieres simplemente probar la aplicación puedes hacerlo con este comando docker run -it -p 7018:7018 -v ${PWD}/Presentation/users.json:/app/Data/users.json -v ${PWD}/Presentation/videogames.json:/app/Data/videogames.json -v ${PWD}/Presentation/logs.json:/app/Logs/logs.json alber965/gamedream:latest</p>
