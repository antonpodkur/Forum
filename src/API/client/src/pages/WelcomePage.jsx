import React from 'react';
import {Link} from "react-router-dom";
import HeaderNotAuthed from "../components/HeaderNotAuthed";

import {Container, Button, Flex, Stack, Text, HStack, VStack, Heading, Center} from "@chakra-ui/react";
import * as PropTypes from "prop-types";

function VStackStack(props) {
    return null;
}

VStackStack.propTypes = {children: PropTypes.node};
const WelcomePage = () => {
    return (
        <div>
            <HeaderNotAuthed></HeaderNotAuthed>
                <Flex alignItems="center">

                    <Container centerContent  py={"10"}>
                        <VStack>
                            <HStack border={"2px"} p={2} borderRadius="md" mb={"2"}>
                                <Text fontWeight={"bold"}>If you have an account -</Text>
                                <Button><Link to={"/login"}>Login</Link></Button>
                            </HStack>
                            <HStack border={"2px"} p={2} borderRadius="md">
                                <Text fontWeight={"bold"}>If you don`t -</Text>
                                <Button><Link to={"/register"}>Register</Link></Button>
                            </HStack>
                        </VStack>
                    </Container>3000
                </Flex>
        </div>
    );
};

export default WelcomePage;