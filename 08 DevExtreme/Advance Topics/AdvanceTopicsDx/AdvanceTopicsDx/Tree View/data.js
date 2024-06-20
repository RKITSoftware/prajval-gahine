const hierarchyData = [{
    "name": "root",
    "type": "folder",
    "children": [
        {
            "name": "documents",
            "type": "folder",
            "children": [
                {
                    "name": "resume.pdf",
                    "type": "file"
                },
                {
                    "name": "cover_letter.docx",
                    "type": "file"
                }
            ]
        },
        {
            "name": "pictures",
            "type": "folder",
            "children": [
                {
                    "name": "vacation",
                    "type": "folder",
                    "children": [
                        {
                            "name": "beach.jpg",
                            "type": "file"
                        },
                        {
                            "name": "mountains.jpg",
                            "type": "file"
                        }
                    ]
                },
                {
                    "name": "family.jpg",
                    "type": "file"
                }
            ]
        },
        {
            "name": "music",
            "type": "folder",
            "children": [
                {
                    "name": "rock",
                    "type": "folder",
                    "children": [
                        {
                            "name": "song1.mp3",
                            "type": "file"
                        },
                        {
                            "name": "song2.mp3",
                            "type": "file"
                        }
                    ]
                },
                {
                    "name": "jazz.mp3",
                    "type": "file"
                }
            ]
        },
        {
            "name": "notes.txt",
            "type": "file"
        }
    ]
}];


const hierarchyData2 = [{
    "id": "1",
    "text": "root",
    "type": "folder",
    "items": [
        {
            "id": "1_1",
            "text": "documents",
            "type": "folder",
            "items": [
                {
                    "id": "1_1_1",
                    "text": "resume.pdf",
                    "type": "file"
                },
                {
                    "id": "1_1_2",
                    "text": "cover_letter.docx",
                    "type": "file"
                }
            ]
        },
        {
            "id": "1_2",
            "text": "pictures",
            "type": "folder",
            "items": [
                {
                    "id": "1_2_1",
                    "text": "vacation",
                    "type": "folder",
                    "items": [
                        {
                            "id": "1_2_1_1",
                            "text": "beach.jpg",
                            "type": "file"
                        },
                        {
                            "id": "1_2_1_2",
                            "text": "mountains.jpg",
                            "type": "file"
                        }
                    ]
                },
                {
                    "text": "family.jpg",
                    "type": "file"
                }
            ]
        },
        {
            "id": "1_3",
            "text": "music",
            "type": "folder",
            "items": [
                {
                    "id": "1_3_1",
                    "text": "rock",
                    "type": "folder",
                    "items": [
                        {
                            "id": "1_3_1_1",
                            "text": "song1.mp3",
                            "type": "file"
                        },
                        {
                            "id": "1_3_1_2",
                            "text": "song2.mp3",
                            "type": "file"
                        }
                    ]
                },
                {
                    "id": "1_3_2",
                    "text": "jazz.mp3",
                    "type": "file"
                }
            ]
        },
        {
            "id": "1_4",
            "text": "notes.txt",
            "type": "file"
        }
    ]
}];


const hierarchyData3 = [
    {
        "id": "root",
        "type": "folder",
        "text": "root",
        "items": [
            {
                "id": "documents",
                "type": "folder",
                "text": "documents",
                "items": [
                    {
                        "Id": "documents\\cover_letter.docx",
                        "type": "file",
                        "name": "cover_letter.docx"
                    },
                    {
                        "Id": "documents\\resume.pdf",
                        "type": "file",
                        "name": "resume.pdf"
                    }
                ]
            },
            {
                "id": "music",
                "type": "folder",
                "text": "music",
                "items": [
                    {
                        "id": "music\\rock",
                        "type": "folder",
                        "text": "rock",
                        "items": [
                            {
                                "Id": "music\\rock\\brownrang.mp3",
                                "type": "file",
                                "name": "brownrang.mp3"
                            },
                            {
                                "Id": "music\\rock\\djwalebabu.mp3",
                                "type": "file",
                                "name": "djwalebabu.mp3"
                            }
                        ]
                    },
                    {
                        "Id": "music\\zindagi.mp3",
                        "type": "file",
                        "name": "zindagi.mp3"
                    }
                ]
            },
            {
                "id": "pictures",
                "type": "folder",
                "text": "pictures",
                "items": [
                    {
                        "id": "pictures\\vacation",
                        "type": "folder",
                        "text": "vacation",
                        "items": [
                            {
                                "Id": "pictures\\vacation\\beach.jpg",
                                "type": "file",
                                "name": "beach.jpg"
                            },
                            {
                                "Id": "pictures\\vacation\\mountains.jpg",
                                "type": "file",
                                "name": "mountains.jpg"
                            }
                        ]
                    },
                    {
                        "Id": "pictures\\family.jpg",
                        "type": "file",
                        "name": "family.jpg"
                    }
                ]
            }
        ]
    }
];