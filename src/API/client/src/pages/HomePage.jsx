import React, {useEffect, useState} from 'react';
import postService from "../services/PostService";

import HeaderAuthed from "../components/HeaderAuthed";
import {Box, Container, Heading, Text} from "@chakra-ui/react";

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
            <HeaderAuthed></HeaderAuthed>
            <Container justifyContent={"center"}>
                {posts.map((post) =>
                    <Box key={post.id} my={4} boxShadow="md" p="2" rounded="md" bg="#AEC5EB">
                        <Heading color={"gray.700"} as="h3" size="lg" my={4}>{post.title}</Heading>
                        <Text ml={5} mb={3}>{post.body}</Text>
                    </Box>
                )}
            </Container>
        </div>
    );
};

export default HomePage;