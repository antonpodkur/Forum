import React, {useContext, useEffect} from 'react';
import {Context} from "../index";
import {Link, useNavigate} from "react-router-dom";
import {observer} from "mobx-react-lite";
import {
    Box,
    Stack,
    Heading,
    Flex,
    Text,
    Button,
    useDisclosure
} from "@chakra-ui/react";
import { HamburgerIcon } from "@chakra-ui/icons";

const HeaderAuthed = (props) => {

    const {userStore} = useContext(Context);
    const navigate = useNavigate();

    const { isOpen, onOpen, onClose } = useDisclosure();
    const handleToggle = () => (isOpen ? onClose() : onOpen());

    const logout = () => {
        userStore.logout();
        navigate("/welcome");
    }

    return (
        <Flex
            as="nav"
            align="center"
            justify="space-between"
            wrap="wrap"
            padding={6}
            bg="#3A405A"
            color="white"
            {...props}
        >
            <Flex align="center" mr={5}>
                <Heading as="h1" size="lg" letterSpacing={"tighter"}>
                    <Link to={"/"}>ONE THOUGHT</Link>
                </Heading>
            </Flex>

            <Box display={{ base: "block", md: "none" }} onClick={handleToggle}>
                <HamburgerIcon />
            </Box>

            <Stack
                direction={{ base: "column", md: "row" }}
                display={{ base: isOpen ? "block" : "none", md: "flex" }}
                width={{ base: "full", md: "auto" }}
                alignItems="center"
                flexGrow={1}
                mt={{ base: 4, md: 0 }}
            >
                <Text>Account</Text>
            </Stack>

            <Box
                display={{ base: isOpen ? "block" : "none", md: "block" }}
                mt={{ base: 4, md: 0 }}
            >
                <Button
                    variant="outline"
                    _hover={{ bg: "#AEC5EB", borderColor: "white", color: "black" }}
                    onClick={() => logout()}
                >
                    Log out
                </Button>
            </Box>
        </Flex>
    );
};

export default observer(HeaderAuthed);
