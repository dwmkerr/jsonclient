//  In-memory collection of posts.
var currentId = 0;
function nextId(){
    return currentId++;
}
var posts = 
[
    {
        id: nextId(),
        title:"Post One", 
        created: new Date(),
        content: "Content of post one."
    },
    {
        id: nextId(),
        title:"Post Two", 
        created: new Date(),
        content: "Content of post two."
    },
    {
        id: nextId(),
        title:"Post Three", 
        created: new Date(),
        content: "Content of post three."
    }
];

exports.getAllPosts = function(request, response){
    response.send(200, posts);
};

exports.getPostById = function(request, response){
	//	Get the post id.
	var id = request.params.id;
    console.log('Retrieving post: ' + id);
    for(var i=0; i<posts.length; i++){
        if(posts[i].id == id){
            response.send(200, posts[i]);
            return;
        }
    }

    //  Couldn't find the post.
    response.send(404, "No post with id: " + id);
};


exports.addPost = function(request, response) {

	//	Get the post, log it.
    var post = request.body;
    console.log('Adding post: ' + JSON.stringify(post));
    post.id = nextId();
    posts.push(post);
    response.send(201, post);
}

exports.updatePost = function(request, response) {

	//	Get the id and post.
    var id = request.params.id;
    var post = request.body;

    //	Log the mission.
    console.log('Updating post: ' + id);
    console.log(JSON.stringify(post));

    for(var i=0; i<posts.length; i++){
        if(posts[i].id == id){
            var keepId = posts[i].id;
            posts[i] = post;
            posts[i].id = keepId;
            console.log('Post updated');
            response.send(200, {'success':'true'});
            return;
        }
    }

    //  Couldn't find the post.
    response.send(404, "No post with id: " + id);

}

exports.deletePost = function(request, response) {

	//	Get the id.
    var id = request.params.id;

    //	Log the post.
    console.log('Deleting post: ' + id);

    for(var i=0; i<posts.length; i++){
        if(posts[i].id == id){
            posts.splice(i, 1);

    console.log('Post deleted');
    response.send(200, {'success':'true'});
            return;
        }
    }
    

    //  Couldn't find the post.
    response.send(404, "No post with id: " + id);
}