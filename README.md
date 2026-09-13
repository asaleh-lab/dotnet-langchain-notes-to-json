# dotnet-langchain-notes-to-json

This repo demonstrates how to turn plain text into structured data. The example we will use is converting casual notes written at the end of a shift in the kitchen into a structured JSON that other parts of an application can deal with. In our case we need a list of actions and owners.

**Article:** [Turn plain-text notes into JSON with LangChain and Flask](https://wysiwygs.de/blog/plain-text-notes-to-json-langchain-flask/)

## Setup

```powershell
dotnet restore
copy .env.example .env
```

Put your OpenAI API key in `.env`.

## Test with one chat call

```powershell
dotnet run --project src -- ChatOnce
```

## Let's fill the prompt template with the notes

```powershell
dotnet run --project src -- PromptTemplate
```
