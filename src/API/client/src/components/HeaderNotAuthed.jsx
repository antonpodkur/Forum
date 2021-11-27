import React, {useContext, useEffect} from 'react';
import {Context} from "../index";
import {Link} from  "react-router-dom";
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

const HeaderNotAuthed = (props) => {

    const {userStore} = useContext(Context);


    const { isOpen, onOpen, onClose } = useDisclosure();
    const handleToggle = () => (isOpen ? onClose() : onOpen());

    return (
        <Flex
            as="nav"
            align="center"
            justify="space-between"
            wrap="wrap"
            padding={6}
            bg="purple.800"
            color="white"
            {...props}
        >
            <Flex align="center" mr={5}>
                <Heading as="h1" size="lg" letterSpacing={"tighter"}>
                    <Link to={"/"}>ONE THOUGHT</Link>
                </Heading>
            </Flex>
        </Flex>
    );
};

export default observer(HeaderNotAuthed);
