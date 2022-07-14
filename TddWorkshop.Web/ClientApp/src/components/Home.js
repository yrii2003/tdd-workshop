import React, {useEffect, useState} from 'react';
import {AutoForm} from "uniforms-material";
import Ajv from 'ajv';
import {JSONSchemaBridge} from 'uniforms-bridge-json-schema';

// const schema = {
//     title: 'Guest',
//     type: 'object',
//     properties: {
//         firstName: {type: 'string', id: 'PersonalInfo_FirstName'},
//         lastName: {type: 'string'},
//         workExperience: {
//             description: 'Work experience in years',
//             type: 'integer',
//             minimum: 0,
//             maximum: 100,
//         },
//     },
//     required: ['firstName', 'lastName'],
// };

function createValidator(schema, ajv) {
    const validator = ajv.compile(schema);

    return (model) => {
        validator(model);
        return validator.errors?.length ? {details: validator.errors} : null;
    };
}

function adaptSchema(parameters){
    const schema = {
        title: 'Form',
        type: 'object',
        properties: {},
        required: []
    };
    
    for(let i = 0; i < parameters.length; i++){
        const key = parameters[i].name.replace('.', '_');
        schema.properties[key] = {
            type: 'string',
            id: key
        };
    }

    const ajv = new Ajv({allErrors: true, useDefaults: true});
    const schemaValidator = createValidator(schema, ajv);
    return new JSONSchemaBridge(schema, schemaValidator);
}

const baseUrl = 'https://localhost:5001';
const swaggerUrl = baseUrl + '/swagger/v1/swagger.json';

function onSubmit(data) {
    console.log(data);
    fetch(baseUrl + '/Calculator/Calculate', {method: 'POST', body: JSON.stringify(data)})
        .then(response => response.json())
        .then(json => {
            console.log(123)
        });
}

export function Home (){
    const [schema, setSchema] = useState(null);
    
    useEffect(() => {
        fetch(swaggerUrl)
            .then(response => response.json())
            .then(json => {
                const parameters = json['components']['schemas']['CreditGoal'];
                setSchema(adaptSchema(parameters));
            })
    }, []);
    
    return !schema 
        ? <div>Loading</div>
        : <AutoForm schema={schema} onSubmit={onSubmit}/>
}