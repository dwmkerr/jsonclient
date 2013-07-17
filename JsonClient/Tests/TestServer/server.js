//	Create the express object.
var express = require('express');

//	Get the routes.
var posts = require('./posts');
//	Create the app.
var app = express();

//	Setup logging.
app.configure(function () {
    app.use(express.logger('dev'));     /* 'default', 'short', 'tiny', 'dev' */
    app.use(express.bodyParser());
});

//	Deal with a bug in Chrome (http://williamjohnbert.com/2013/06/allow-cors-with-localhost-in-chrome/)
app.all("/*", function(req, res, next) {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Headers", "Cache-Control, Pragma, Origin, Authorization, Content-Type, X-Requested-With");
  res.header("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
  console.log('Adding access control headers to the response.')
  return next();
});

//	Create the posts handlers.
app.get('/posts', posts.getAllPosts);
app.get('/posts/:id', posts.getPostById);
app.post('/posts', posts.addPost);
app.put('/posts/:id', posts.updatePost);
app.delete('/posts/:id', posts.deletePost);

//	The port to use.
var port = 3212;

//	Listen on 3000 and log.
app.listen(port);
console.log('Listening on port ' + port);