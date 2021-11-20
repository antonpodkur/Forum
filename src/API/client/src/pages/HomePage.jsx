import React, {useEffect, useState} from 'react';
import postService from "../services/PostService";

const HomePage = () => {
    const [posts, setPosts] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect( () => {
        async function fetchData() {
            try{
                const posts = await postService.getAll();
                setPosts(posts.data);
                setLoading(false);
                console.log(posts);
            } catch(e) {
                console.log(e.message);
            }
        }
        fetchData();
    }, []);

    if(loading) return <div>Loading</div>
    return (
        <div>
            <h1>Home page</h1>
            {posts.map((post) =>
                <li key={post.id}>
                    {post.title}
                </li>
            )}
        </div>
    );
};

export default HomePage;