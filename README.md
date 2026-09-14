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

## Fill the prompt template with the notes

```powershell
dotnet run --project src -- PromptTemplate
```

## Now the parser comes in

```powershell
dotnet run --project src -- ParseJson
```

## Serve it with a small HTTP server

```powershell
dotnet run --project src -- App
```

Open http://127.0.0.1:5000

## Compare two models

```powershell
dotnet run --project src -- CompareModels
```

Same note, same prompt, same parser. Only the model name changes (`gpt-4o-mini` then `gpt-3.5-turbo`). You should see two objects. Wording can differ but the keys should not.

This article is a practical implementation of the concepts in [Develop Generative AI Applications: Get Started](https://www.coursera.org/learn/develop-generative-ai-applications-get-started).
