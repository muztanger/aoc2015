# Script to migrate inline input strings to external text files

# Get the script directory
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectDir = Join-Path $scriptDir "Advent of Code 2015"
$inputsDir = Join-Path $projectDir "Inputs"

# Create Inputs directory if it doesn't exist
if (-not (Test-Path $inputsDir)) {
    New-Item -ItemType Directory -Path $inputsDir | Out-Null
    Write-Host "Created Inputs directory: $inputsDir"
}

# Find all Day*.cs files
$dayFiles = Get-ChildItem -Path $projectDir -Filter "Day*.cs"

foreach ($file in $dayFiles) {
    Write-Host "`nProcessing $($file.Name)..."
    
    # Read the file content
    $content = Get-Content $file.FullName -Raw
    
    # Check if file contains static string inputString with @"..." pattern
    if ($content -match 'static\s+string\s+inputString\s*=\s*@"([^"]*(?:""[^"]*)*)"') {
        # Extract the day number from filename (Day05.cs -> 05)
        $dayNumber = $file.BaseName -replace 'Day', ''
        $txtFileName = "day$dayNumber.txt"
        $txtFilePath = Join-Path $inputsDir $txtFileName
        
        # Extract the string content (the part between @" and ")
        $stringContent = $matches[1]
        
        # Remove the leading newline if present and convert escaped quotes
        $stringContent = $stringContent -replace '^[\r\n]+', ''
        $stringContent = $stringContent -replace '""', '"'
        
        # Write the content to the text file
        Set-Content -Path $txtFilePath -Value $stringContent -NoNewline
        Write-Host "  Created: $txtFileName"
        
        # Replace the static string declaration in the C# file
        $newContent = $content -replace 'static\s+string\s+inputString\s*=\s*@"[^"]*(?:""[^"]*)*";', 
            "private static readonly string inputString = InputLoader.ReadAllText(`"$txtFileName`");"
        
        # Save the modified C# file
        Set-Content -Path $file.FullName -Value $newContent -NoNewline
        Write-Host "  Modified: $($file.Name) to use InputLoader"
    }
    elseif ($content -match 'InputLoader\.ReadAllText') {
        Write-Host "  Already using InputLoader - skipping"
    }
    else {
        Write-Host "  No static inputString found - skipping"
    }
}

Write-Host "`nMigration complete!"