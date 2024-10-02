import React, {useEffect, useState} from "react";
const FeedPage = () => {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        const fetchPosts = async () => {
            try {
                const response = await fetch('http://localhost:5000/api/content?creatorId=19');
                if (!response.ok) {
                    throw new Error('Failed to fetch posts');
                }
                const data = await response.json();
                setPosts(data);
            } catch (error) {
                console.error('Error fetching posts:', error);
            }
        };
        fetchPosts();
    }, []);
    return (
        <div>
            <h1>Feed Page</h1>
            <div className="post-container">
                {posts.length > 0 ? (
                    posts.map((post) => (
                        <div key={post.id} className="post">
                            <h2>{post.title}</h2>
                            <p>{post.description}</p>
                            {post.imageUrl && (
                                <img
                                    src={`http://localhost:5000${post.imageUrl}`}
                                    alt={post.title}
                                    style={{ width: '200px', height: '200px' }}
                                />
                            )}
                            <p>Posted on: {new Date(post.creationTime).toLocaleDateString()}</p>
                        </div>
                    ))
                ) : (
                    <p>No posts available</p>
                )}
            </div>
        </div>
    )

}

export default FeedPage;